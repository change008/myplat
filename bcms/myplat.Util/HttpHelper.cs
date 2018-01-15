using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    //public static TResult PostResultByJson<TResult>(string url, object postJsonData, string contentType = "application/x-www-form-urlencoded")
    //    {
    //        return PostResultByJson<TResult>(url, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postJsonData)), contentType);
    //    }

    internal static class HttpHelper
    {
        public static byte[] Get(string url)
        {
            WebHeaderCollection responseHeaders;
            return Get(url, out responseHeaders);
        }

        public static string GetString(string url)
        {
            return Encoding.UTF8.GetString(Get(url));
        }

        public static byte[] Get(string url, out WebHeaderCollection responseHeaders)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    responseHeaders = response.Headers;
                    return responseStream.ReadBytes();
                }
            }
        }

        public static byte[] Post(string url, byte[] postData, string contentType = "application/x-www-form-urlencoded")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        return responseStream.ReadBytes();
                    }
                }
            }
        }

    }
}
