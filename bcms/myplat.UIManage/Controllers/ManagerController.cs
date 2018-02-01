using log4net;
using myplat.Biz;
using myplat.Entity;
using myplat.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myplat.UIManage.Controllers
{
    public class ManagerController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        private static ILog _logger = LogManager.GetLogger(typeof(ManagerController));

        /// <summary>
        /// 管理biz
        /// </summary>
        private static ManagerBiz _ManagerBiz = new ManagerBiz();
        public static string _CookieName = ConfigurationManager.AppSettings["CookieName"];

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!_ManagerBiz.VerificationAdmin(_CookieName))
            {
                Response.Redirect("/Account/Login");
            }
            try
            {
                IEnumerable<Manager> list = _ManagerBiz.GetList();
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return View();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public JsonResult Edit()
        {
            try
            {
                int opType = int.Parse(Request["PageOpType"].ToString());
                switch (opType)
                {
                    case 1://添加

                        Manager model = new Manager();
                        model.Name = Request["Name"];
                        string pwd = Request["Pwd"];
                        DateTime createTime;
                        if (!DateTime.TryParse(Request["CreateTime"], out createTime))
                        {
                            createTime = DateTime.Now;
                        }
                        model.Password =pwd;
                        model.CreateTime = createTime;
                        _ManagerBiz.Add(model);
                        return Json(new { result = true, msg = "" });

                    case 2://编辑
                        Manager modal = _ManagerBiz.Get(int.Parse(Request["Id"]));
                        if (modal == null)
                        {
                            return Json(new { result = true, Msg = "ID不存在" });
                        }
                        modal.Name = Request["Name"];
                        DateTime createTimeEdit;
                        if (!DateTime.TryParse(Request["CreateTime"], out createTimeEdit))
                        {
                            createTimeEdit = DateTime.Now;
                        }
                        modal.CreateTime = createTimeEdit;
                        _ManagerBiz.Update(modal);
                        return Json(new { result = true, msg = "" });
                    default:
                        throw new Exception("操作类型错误");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Json(new { result = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Get(int id)
        {
            var ret = _ManagerBiz.Get(id);
            if (ret != null)
            {
                ret.CreateTimeStr = ret.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            }
            return Json(ret);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public JsonResult Reset()
        {
            try
            {
                Manager model = _ManagerBiz.Get(int.Parse(Request["Id"]));
                if (model == null)
                {
                    return Json(new { result = true, Msg = "ID不存在" });
                }
                string pwd = "borui2017fa";
                _ManagerBiz.UpdatePwd(model.Id, pwd);
                return Json(new { result = true, msg = "" });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Json(new { result = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                _ManagerBiz.Del(id);
                return Json(new { result = true, msg = "" });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Json(new { result = false, msg = ex.Message });
            }
        }

        public ActionResult ChangeIdentity()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeIdentity(FormCollection collection)
        {
            string name = collection["name"].ToString();
            string pwd = collection["old_pwd"].ToString();
            string new_pwd = collection["new_pwd"].ToString();
            Manager user = _ManagerBiz.GetModelByName(name);
            if (user != null)
            {
                if (MD5Encrypt.Md5Hex(pwd).Equals(user.Password))
                {
                    bool ret = _ManagerBiz.UpdatePwd(user.Id, new_pwd);
                    if (!ret)
                    {
                        ModelState.AddModelError("", "密码修改错误，请联系管理员");
                    }
                    else
                    {
                        ModelState.AddModelError("", "修改成功，请返回重新登录");
                        ViewBag.Flag = "修改成功";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "密码错误,请重新输入");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户不存在");
            }

            return View();
        }

    }
}