using myplat.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myplat.UIManage.Controllers
{
    public class HomeController : Controller
    {
        //管理
        private readonly static ManagerBiz _ManagerBiz = new ManagerBiz();

        public ActionResult Index()
        {
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }
            return View();
        }




    }
}