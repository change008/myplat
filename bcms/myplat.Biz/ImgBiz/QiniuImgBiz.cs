using log4net;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace myplat.Biz
{
    public class QiniuImgBiz
    {

        /// <summary>
        /// logger
        /// </summary>
        private static ILog _logger = LogManager.GetLogger(typeof(QiniuImgBiz));

        //ext
        private readonly static string[] _imageExtensions = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

        //accesskey & secretKey
        private static string AccessKey = ConfigurationManager.AppSettings["QiniuAccessKey"];
        private static string SecretKey = ConfigurationManager.AppSettings["QiniuSecretKey"];

        //默认bucket
        private static string DefaultBucket = ConfigurationManager.AppSettings["DefaultBucket"];

        //图片访问前缀
        private static string ImageUrlPrefix = ConfigurationManager.AppSettings["ImageUrlPrefix"];


        public bool UploadStream(Stream stream, string fileName, out string address)
        {
            address = "";
            if (stream == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }
            try
            {
                var extensionName = fileName.Substring(fileName.LastIndexOf("."));
                if (!_imageExtensions.Contains(extensionName.ToLower()))
                {
                    return false;
                }
                var generateFileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff-")}{Guid.NewGuid().GetHashCode().ToString().Replace("-", string.Empty)}{extensionName}";
                var saveKey = $"default/{generateFileName}";

                // 生成(上传)凭证时需要使用此Mac
                // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
                // 实际应用中，请自行设置您的AccessKey和SecretKey
                Mac mac = new Mac(QiniuImgBiz.AccessKey, QiniuImgBiz.SecretKey);


                // 上传策略，参见 
                // https://developer.qiniu.com/kodo/manual/put-policy
                PutPolicy putPolicy = new PutPolicy();

                // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
                // putPolicy.Scope = bucket + ":" + saveKey;
                putPolicy.Scope = DefaultBucket;


                // 上传策略有效期(对应于生成的凭证的有效期)   
                putPolicy.SetExpires(3600);


                // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
                //putPolicy.DeleteAfterDays = 1;
                // 生成上传凭证，参见
                // https://developer.qiniu.com/kodo/manual/upload-token            
                string jstr = putPolicy.ToJsonString();
                string token = Auth.CreateUploadToken(mac, jstr);

                FormUploader fu = new FormUploader();
                var result = fu.UploadStream(stream, saveKey, token);
                if (result.Code == 200)
                {
                    address = ImageUrlPrefix + saveKey;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false; 
            }
        }


        public bool UploadUri(string uri, out string address)
        {
            address = "";
            if (string.IsNullOrEmpty(uri))
            {
                return false;
            }

            try
            {
                //var extensionName = fileFullPath.Substring(fileFullPath.LastIndexOf("."));
                string extensionName = Path.GetExtension(uri);

                if (string.IsNullOrEmpty(extensionName) || !_imageExtensions.Contains(extensionName))
                {
                    //后缀没有成功获取的情况下,我们要
                    extensionName = ".jpeg";
                }

                var generateFileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff-")}{Guid.NewGuid().GetHashCode().ToString().Replace("-", string.Empty)}{extensionName}";
                var saveKey = $"default/{generateFileName}";


                // 生成(上传)凭证时需要使用此Mac
                // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
                // 实际应用中，请自行设置您的AccessKey和SecretKey
                Mac mac = new Mac(QiniuImgBiz.AccessKey, QiniuImgBiz.SecretKey);


                // 上传策略，参见 
                // https://developer.qiniu.com/kodo/manual/put-policy
                PutPolicy putPolicy = new PutPolicy();

                // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
                // putPolicy.Scope = bucket + ":" + saveKey;
                putPolicy.Scope = DefaultBucket;


                // 上传策略有效期(对应于生成的凭证的有效期)   
                putPolicy.SetExpires(3600);


                // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
                //putPolicy.DeleteAfterDays = 1;
                // 生成上传凭证，参见
                // https://developer.qiniu.com/kodo/manual/upload-token            
                string jstr = putPolicy.ToJsonString();
                string token = Auth.CreateUploadToken(mac, jstr);

                //上传
                using (Stream thumbnailStream = new MemoryStream(new WebClient().DownloadData(uri)))
                {

                    FormUploader fu = new FormUploader();
                    var result = fu.UploadStream(thumbnailStream, saveKey, token);
                    if (result.Code == 200)
                    {
                        address = ImageUrlPrefix + saveKey;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 文本中的图片本地化
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns>new_content</returns>
        public string LocalizationImgInTxt(string content)
        {
            Regex reg = new Regex(@"<img[^>]*(data-)?src=['""\s]*([^\s'""]+)[^>]*>");
            List<string> imageList = new List<string>();

            MatchCollection matches = reg.Matches(content);
            foreach (Match m in matches)
            {
                string url = reg.Replace(m.Value, "$2");
                if (url.ToLower().Contains(ImageUrlPrefix)) //如果已经是放在我们七牛上的图片就不上传
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(url) && (url.StartsWith("http://") || url.StartsWith("https://")))
                {
                    if (imageList.IndexOf(url) == -1)
                    {
                        imageList.Add(url);
                        string addr = "";
                        UploadUri(url, out addr);
                        content = content.Replace(m.Value, string.Format("<img src=\"{0}\" />", addr));
                    }
                    else
                    {
                        //_logger.Error("----LocalizationImgInTxt---11111111111 url: " + url);
                    }
                }
                else
                {
                    //_logger.Error("----LocalizationImgInTxt---2222222222 url: " + url);
                }
            }

            return content;
        }


    }
}
