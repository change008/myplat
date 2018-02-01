using myplat.Biz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myplat.UIManage.Controllers
{
    public class HomeController : Controller
    {
        //管理
        private readonly static ManagerBiz _ManagerBiz = new ManagerBiz();
        public static string _CookieName = ConfigurationManager.AppSettings["CookieName"];

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!_ManagerBiz.VerificationAdmin(_CookieName))
            {
                Response.Redirect("/Account/Login");
            }
            return View();
        }






    }
}