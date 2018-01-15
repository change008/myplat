using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace myplat.Util
{
    public class CommonUtil
    {
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetIP()
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress) || !IsIP(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

       


        /// <summary>
        /// list转化为Json
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return "[]";
            }
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 判断是否为手机平台
        /// </summary>
        /// <returns></returns>
        public static bool IsMobliePlatform(HttpRequestBase request)
        {
            string agent = (request.UserAgent + "").ToLower().Trim();

            if (agent == "" ||
                agent.IndexOf("mobile") != -1 ||
                agent.IndexOf("mobi") != -1 ||
                agent.IndexOf("nokia") != -1 ||
                agent.IndexOf("samsung") != -1 ||
                agent.IndexOf("sonyericsson") != -1 ||
                agent.IndexOf("mot") != -1 ||
                agent.IndexOf("blackberry") != -1 ||
                agent.IndexOf("lg") != -1 ||
                agent.IndexOf("htc") != -1 ||
                agent.IndexOf("j2me") != -1 ||
                agent.IndexOf("ucweb") != -1 ||
                agent.IndexOf("opera mini") != -1 ||
                agent.IndexOf("mobi") != -1 ||
                agent.IndexOf("android") != -1 ||
                agent.IndexOf("iphone") != -1)
            {
                //终端可能是手机

                return true;

            }

            return false;
        }

        /// <summary>
        /// 不区分大小写替换
        /// </summary>
        /// <param name="realaceValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceNoCase(string realaceValue, string oldValue, string newValue)
        {
            return Microsoft.VisualBasic.Strings.Replace(realaceValue, oldValue, newValue, 1, -1, CompareMethod.Text);
        }

    }

}
