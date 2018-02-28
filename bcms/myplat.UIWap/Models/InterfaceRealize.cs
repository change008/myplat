using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace myplat.UIWap.Models
{
    public class InterfaceRealize
    {
        private static ILog _logger = LogManager.GetLogger(typeof(InterfaceRealize));
        /// <summary>
        /// 获取微信导量文章
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<WxThreadModel> GetWxThread(string url)
        {
            var resultData = new List<WxThreadModel>();
            try
            {
                WebClient wc = new WebClient();
                var bRequest= wc.DownloadData(url);
                return JsonConvert.DeserializeObject<List<WxThreadModel>>(Encoding.UTF8.GetString(bRequest));
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("GetWxThread 获取微信导量文章报错。{0}",ex.Message);
                return resultData;
            }

        }
    }

    public class WxThreadModel
    {
        public int Id { get; set; }
        public List<string> Covers { get; set; }
        public string Title { get; set; }
        public string ImageList { get; set; }
        public string Content { get; set; }
        public string Intro { get; set; }
    }


}