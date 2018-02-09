using log4net;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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





    }
}
