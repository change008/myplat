using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myplat.UIManage.Filters;
using myplat.Biz;
using System.Configuration;
using myplat.UIManage.Models;
using myplat.Entity;

namespace myplat.UIManage.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //cookie管理
        private static readonly UserAuthCookie _authnService = new UserAuthCookie();
        private static readonly ManagerBiz _managerBiz = new ManagerBiz();

        public static string _CookieName = ConfigurationManager.AppSettings["CookieName"];

        /// <summary>
        /// 返回登录界面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            model.UserName = HttpUtility.UrlDecode(model.UserName);
            int userId = 0;
            Manager admin = _managerBiz.Check(model.UserName, model.Password, out userId);
            if (ModelState.IsValid)
            {
                if (admin != null)
                {
                    _authnService.SignIn(_CookieName, model.UserName, userId, validityDays: 10);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或者密码错误");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginOut()
        {
            _authnService.SignOut(_CookieName);
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 获取权限列表--暂不使用
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetAuthority()
        {
            try
            {
                var userId = _authnService.GetUserID(_CookieName);
                Manager model = _managerBiz.Get(userId);
                return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }


    }
}
