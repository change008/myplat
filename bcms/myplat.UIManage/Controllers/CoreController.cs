using log4net;
using myplat.Biz;
using myplat.Entity;
using myplat.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace myplat.UIManage.Controllers
{
    /// <summary>
    /// 核心内容Controller
    /// </summary>
    public class CoreController : Controller
    {
        private static ILog _logger = LogManager.GetLogger(typeof(CoreController));

        /// <summary>
        /// 内容管理
        /// </summary>
        private static CoreBiz _coreBiz = new CoreBiz();
        private readonly static ManagerBiz _ManagerBiz = new ManagerBiz();
        public static string _CookieName = ConfigurationManager.AppSettings["CookieName"];

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            int pageSize = 50;
            int totalNum = 1;//内容总条目数
            int pageIndex = Request.QueryString["p"] == null ? 1 : int.Parse(Request.QueryString["p"]);

            //搜索， Id, Title, Type, Stauts
            string idStr = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            string title = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString();
            string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
            string status = Request.QueryString["status"] == null ? "" : Request.QueryString["status"].ToString();

            string strWhere = " where 1=1 ";

            //id
            int id;
            if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out id))
            {
                strWhere += " and Id = " + idStr;
            }

            //title
            if (!string.IsNullOrEmpty(title.Trim()))
            {
                strWhere += " and Title like '%" + title + "%' ";
            }

            //类型
            if (!string.IsNullOrEmpty(type) && int.Parse(type) > 0)
            {
                strWhere += " and Type = " + type;
            }

            //状态
            if (!string.IsNullOrEmpty(status) && int.Parse(status) > 0)
            {
                strWhere += " and Status = " + status;
            }


            var contentList = _coreBiz.GetList(pageIndex, pageSize, strWhere);
            totalNum = _coreBiz.GetCount(strWhere);

            ViewData["PageSize"] = pageSize;
            ViewData["Total"] = totalNum;

            //查询搜索字段
            ViewBag.Id = idStr;
            ViewBag.Title = title;
            ViewBag.Type = type;
            ViewBag.Status = status;

            return View(contentList);
        }



        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult Detail(int id = 0)
        {
            if (id <= 0)
            {
                RedirectToAction("Index");
            }
            // 文章
            Core article = _coreBiz.Get(id);

            return View(article);
        }



        /// <summary>
        /// 添加文章
        /// </summary>
        public ActionResult Add()
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }
            return View();
        }


        /// <summary>
        /// 添加数据操作
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            //构建文章Model
            Core model = new Core();
            try
            {
                model.Title = collection["Title"].ToString();
                model.Intro = collection["Intro"].ToString();

                model.HContent = collection["BodyContent"].ToString();

                model.Imgs = string.IsNullOrEmpty(collection["Cover_ImgDefault"]) ? "[]" : collection["Cover_ImgDefault"];//封面图;
                //model.ImgList = JsonConvert.DeserializeObject<List<string>>(string.IsNullOrEmpty(model.Imgs) ? "[]" : model.Imgs);//对象已经处理
                model.Cover = "";
                if (model.ImgList != null && model.ImgList.Count() > 0)
                {
                    model.Cover = model.ImgList.First();
                }

                //类型 数据类型:(1：帖子 2：资讯 3：视频)
                model.Type = int.Parse(collection.Get("Type"));
                model.Status = Core.CoreStatus_Normal;

                model.FrameLink = collection["FrameLink"].ToString(); //框架链接，暗访
                model.OriginalLink = collection["OriginalLink"].ToString(); //原文链接
                model.RedirectLink = collection["RedirectLink"].ToString(); //自动跳转链接

                model.CreateTime = DateTime.Now;
                model.ShowTime = DateTime.Now;

                model.ViewCount = 0;
                model.DingCount = 0;


                //发布文章数据
                int articleId = _coreBiz.Add(model);

                ViewBag.Msg = "";

            }
            catch (Exception ex)
            {
                _logger.Error("操作失败,信息填写完整及保证正确的格式,谢谢", ex);
                ViewBag.Msg = ex.Message;
                return View(model);
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage()
        {
            HttpFileCollectionBase files = Request.Files;
            // bool isWideImage = Request["Select_ImgShowType"] == "1";
            Dictionary<string, string> result = new Dictionary<string, string>();
            HttpPostedFileBase image1 = (HttpPostedFileBase)Request.Files["img1"];
            HttpPostedFileBase image2 = (HttpPostedFileBase)Request.Files["img2"];
            HttpPostedFileBase image3 = (HttpPostedFileBase)Request.Files["img3"];

            ImgFormat format = null;


            format = new ImgFormat(1, ImgFormatType.Spec, 140, 90);


            if (image1 != null)
            {
                ImgUploadRet imgRet = ImgUtil.UploadImag(image1, format);
                if (imgRet.IsSuc)
                {
                    result["img1"] = imgRet.GetImgUrl(1);
                }
            }
            if (image2 != null)
            {
                ImgUploadRet imgRet = ImgUtil.UploadImag(image2, format);
                if (imgRet.IsSuc)
                {
                    result["img2"] = imgRet.GetImgUrl(1);
                }
            }
            if (image3 != null)
            {
                ImgUploadRet imgRet = ImgUtil.UploadImag(image3, format);
                if (imgRet.IsSuc)
                {
                    result["img3"] = imgRet.GetImgUrl(1);
                }
            }

            return Json(result);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            try
            {
                Core model = null;
                if (id > 0)
                {
                    try
                    {
                        model = _coreBiz.Get(id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrMsg = "请求数据失败" + ex.Message;
                        _logger.Error(ex);
                    }
                }
                else
                {
                    ViewBag.ErrMsg = "参数错误";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                ViewBag.ErrMsg = "参数错误" + ex.Message;
                return View();
            }
        }


        /// <summary>
        /// 编辑提交
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            try
            {
                Core model = null;
                if (id > 0)
                {
                    try
                    {
                        model = _coreBiz.Get(id);

                        model.Title = collection["Title"].ToString();
                        model.Intro = collection["Intro"].ToString();

                        model.HContent = collection["BodyContent"].ToString();

                        model.Imgs = string.IsNullOrEmpty(collection["Cover_ImgDefault"]) ? "[]" : collection["Cover_ImgDefault"];//封面图;
                                                                                                                                  //model.ImgList = JsonConvert.DeserializeObject<List<string>>(string.IsNullOrEmpty(model.Imgs) ? "[]" : model.Imgs);//对象已经处理
                        model.Cover = "";
                        if (model.ImgList != null && model.ImgList.Count() > 0)
                        {
                            model.Cover = model.ImgList.First();
                        }

                        //类型 数据类型:(1：帖子 2：资讯 3：视频)
                        model.Type = int.Parse(collection.Get("Type"));
                        model.Status = Core.CoreStatus_Normal;

                        model.FrameLink = collection["FrameLink"].ToString(); //框架链接，暗访
                        model.OriginalLink = collection["OriginalLink"].ToString(); //原文链接
                        model.RedirectLink = collection["RedirectLink"].ToString(); //自动跳转链接

                        model.ViewCount = 0;
                        model.DingCount = 0;


                        //更新数据
                        bool flag = _coreBiz.Update(model);
                        if (flag)
                        {

                            ModelState.AddModelError("", "文章修改成功");
                            ViewBag.Success = true;
                            ViewBag.Msg = "文章修改成功";
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError("", "文章修改失败");
                            ViewBag.Success = false;
                            ViewBag.Msg = "文章修改失败";
                            return View(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Success = false;
                        ViewBag.Msg = "请求数据失败" + ex.Message;
                        _logger.Error(ex);
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Msg = "参数错误";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Msg = "出现异常:" + ex.Message;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            if (id > 0)
            {
                bool result = false;

                try
                {
                    result = _coreBiz.Del(id);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    return Json(new { Ok = false, Msg = ex.Message });
                }
                if (result)
                {
                    return Json(new { Ok = true });
                }
                else
                {
                    return Json(new { Ok = false, Msg = "删除失败" });
                }
            }
            else
            {
                return Json(new { Ok = false, Msg = "参数错误" });
            }
        }


        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recover(int id, FormCollection collection)
        {
            //验证管理员权限
            if (!_ManagerBiz.VerificationAdmin())
            {
                Response.Redirect("/Account/Login");
            }

            if (id > 0)
            {
                bool result = false;

                try
                {
                    result = _coreBiz.UpdateStatus(id, Core.CoreStatus_Normal);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    return Json(new { Ok = false, Msg = ex.Message });
                }
                if (result)
                {
                    return Json(new { Ok = true });
                }
                else
                {
                    return Json(new { Ok = false, Msg = "恢复数据失败" });
                }
            }
            else
            {
                return Json(new { Ok = false, Msg = "参数错误" });
            }
        }

        /// <summary>
        /// 深度清理html, 洗p, img, br标签 样式
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>

        [ValidateInput(false)]
        public string DeepClear(string htmlString = "")
        {
            if (!string.IsNullOrEmpty(htmlString))
            {
                htmlString = HTMLUtil.RemoveHtmlTag(htmlString, "p", "img", "br");
                htmlString = HTMLUtil.ClearImgTag(htmlString);
                Regex contentRegex = new Regex(@"(style="".*?"")");
                htmlString = contentRegex.Replace(htmlString, "");
            }
            return htmlString;
        }

        /// <summary>
        /// 本地化图片操作
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string LockiconImg(string htmlString = "")
        {
            try
            {
                //htmlString = _clientBiz.LocalizationImgInTxt(htmlString);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("本地化失败。{0}", ex.Message);
            }
            return htmlString;

        }
    }
}