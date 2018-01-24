using myplat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;


namespace myplat.Biz
{
    /// <summary>
    /// WAP Cookie 用户验证
    /// </summary>
    public class UserAuthCookie
    {
        //密钥
        private static readonly string encryptKey = "akajshdflkiwbl";

        /// <summary>
        /// Cookie登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <param name="createPersistentCookie"></param>
        /// <param name="validityDays"></param>
        public void SignIn(string cookieName, string userName, int userId, bool createPersistentCookie = false, int validityDays = 10)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new Exception("用户名为空");
            }
            var utcIssueDate = DateTime.UtcNow;
            var utcExpireDate = utcIssueDate.AddDays((validityDays <= 0) ? 1 : validityDays);

            long issueTimeStamp = DateTimeUtil.GetTimeStamp(utcIssueDate);
            long expireTimeStamp = DateTimeUtil.GetTimeStamp(utcExpireDate);

            StringBuilder cookieValue = new StringBuilder();
            cookieValue.Append("UserID=" + userId + "&");
            cookieValue.Append("UserName=" + HttpUtility.UrlEncode(userName) + "&");
            cookieValue.Append("IssueDate=" + issueTimeStamp + "&");
            cookieValue.Append("ExpireDate=" + expireTimeStamp + "&");

            cookieValue.Append("sign=" + MD5Encrypt.Md5Hex(userId.ToString() + userName.ToString() + issueTimeStamp.ToString() + expireTimeStamp.ToString() + encryptKey));

            SymmetricEncrypt symenc = new SymmetricEncrypt();
            string cookiestr = symenc.Encrypto(cookieValue.ToString());

            string domain = "";
            try
            {
                domain = GetDomain(HttpContext.Current.Request.Url);
            }
            catch
            {
                domain = "a.cn";
            }

            HttpCookie authCookie = new HttpCookie(cookieName, cookiestr);
            authCookie.Path = "/";
            if (domain != "localhost")
                authCookie.Domain = domain;
            authCookie.Expires = utcExpireDate;
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }


        /// <summary>
        /// 退出
        /// </summary>
        public void SignOut(string cookieName)
        {
            string domain = "";
            try
            {
                domain = GetDomain(HttpContext.Current.Request.Url);
            }
            catch {
                domain = "a.cn";
            }


            HttpCookie authCookie = new HttpCookie(cookieName, "user_info");
            authCookie.Path = "/";
            authCookie.Domain = domain;
            authCookie.Expires = DateTime.Now.AddYears(-10);
            HttpContext.Current.Response.Cookies.Add(authCookie);
            HttpContext.Current.Request.Cookies.Remove(cookieName);
            
            FormsAuthentication.SignOut();
        }


        /// <summary>
        /// 判断用户是否登陆
        /// </summary>
        /// <returns></returns>
        public bool IsLogin(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null || String.IsNullOrWhiteSpace(cookie.Value)) return false;


            #region 开始自已写cookie
            SymmetricEncrypt symenc = new SymmetricEncrypt();
            var cookievalue = symenc.Decrypto(cookie.Value.Trim());
            int uid;
            int validay;
            string uname;
            return ValidityCookie(cookievalue, out uid, out validay, out uname);

            #endregion
        }


        /// <summary>
        /// 获得Cookie中保存的用户昵称
        /// </summary>
        /// <returns></returns>
        public string GetUserName(string cookieName)
        {
            int userId = 0;
            int validityDays = 0;
            string uname = "";
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null || String.IsNullOrWhiteSpace(cookie.Value)) return "";


            #region 开始自已写cookie
            SymmetricEncrypt symenc = new SymmetricEncrypt();
            var cookievalue = symenc.Decrypto(cookie.Value.Trim());


            ValidityCookie(cookievalue, out userId, out validityDays, out uname);
            return uname;
            #endregion
        }

        /// <summary>
        /// 获得Cookie中保存的用户ID
        /// </summary>
        /// <returns></returns>
        public int GetUserID(string cookieName)
        {
            int userId = 0;
            int validityDays = 0;
            string uname = "";
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null || String.IsNullOrWhiteSpace(cookie.Value)) return 0;


            #region 开始自已写cookie
            SymmetricEncrypt symenc = new SymmetricEncrypt();
            var cookievalue = symenc.Decrypto(cookie.Value.Trim());


            ValidityCookie(cookievalue, out userId, out validityDays, out uname);
            return userId;
            #endregion
        }

        /// <summary>
        /// 判断用户是否为有效登陆用户及输出登陆ID和有效时间
        /// </summary>
        /// <param name="cookieValue"></param>
        /// <param name="userId"></param>
        /// <param name="validityDays"></param>
        /// <returns></returns>
        private bool ValidityCookie(string cookieValue, out int userId, out int validityDays, out string userName)
        {
            userId = 0;
            validityDays = 0;
            userName = "";
            int uid = 0;
            string uname = "";
            long IssueDate = 0;
            long ExpireDate = 0;
            string Sign = "";
            if (cookieValue != "")
            {
                string[] strQuery = cookieValue.Split('&');
                foreach (string strParam in strQuery)
                {
                    string[] splitParam = strParam.Split('=');
                    try
                    {
                        switch (splitParam[0])
                        {
                            case "UserID"://用户数字ID
                                {
                                    uid = Int32.Parse(splitParam[1]);
                                    break;
                                }
                            case "UserName"://用户名称
                                {
                                    uname = splitParam[1];
                                    break;
                                }
                            case "IssueDate"://用户数字ID
                                {
                                    IssueDate = Convert.ToInt64(splitParam[1]);
                                    break;
                                }
                            case "ExpireDate"://过期时间
                                {
                                    ExpireDate = Convert.ToInt64(splitParam[1]);
                                    break;
                                }
                            case "sign"://
                                {
                                    Sign = splitParam[1];
                                    break;
                                }
                        }
                    }
                    catch
                    {

                    }
                }

                if (Sign == MD5Encrypt.Md5Hex(uid.ToString() + HttpUtility.UrlDecode(uname).ToString() + IssueDate.ToString() + ExpireDate.ToString() + encryptKey))
                {
                    userId = uid;
                    userName = uname;
                    validityDays = (DateTimeUtil.FormatDateTime(ExpireDate) - DateTimeUtil.FormatDateTime(IssueDate)).Days;
                    if (validityDays <= 0) validityDays = 1;
                    return true;
                }

            }
            return false;
        }


        private string GetDomain(Uri host)
        {
            if (host.HostNameType == UriHostNameType.Dns)
            {
                string domain = host.Host;

                string[] domainsplit = domain.Split(new char[] { '.' });
                int domainlength = domainsplit.Length;
                if (domainlength > 1)
                {
                    return domainsplit[domainlength - 2] + "." + domainsplit[domainlength - 1];
                }
                else
                {
                    return domain;
                }
            }
            else
            {
                return host.Host;
            }
        }

    }
}
