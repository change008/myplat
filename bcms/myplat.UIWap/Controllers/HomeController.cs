using log4net;
using myplat.Biz;
using myplat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace myplat.UIWap.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// 缓存时间-秒
        /// </summary>
        private const int cacheDurationCommon = 180;
        private static int COUNTER = 0;
        private static int COUNTER2 = 0;

        /// <summary>
        /// log4net
        /// </summary>
        private static ILog _logger = LogManager.GetLogger(typeof(HomeController));

        //文章biz
        private static CoreBiz _CoreBiz = new CoreBiz();

        //random
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        [OutputCache(Duration = cacheDurationCommon, VaryByCustom = "*", Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 帖子详情页
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = cacheDurationCommon, VaryByCustom = "*", Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Thread(int id = 0, int rowNum = 0, string umparam = "", int thread = 0)
        {
            if (id == 0)
            {
                return Redirect("http://t.cn/REO9Wkz");
            }

            var model = _CoreBiz.Get(id);
            if (model == null)
            {
                Response.RedirectToRoute(new { controller = "Home", action = "Index" });
                return View(model);
            }

            //获取更多相关文章逻辑
            var articleList = _CoreBiz.GetList(1, 10, "");
            

            //广告1
            Core adCore1 = new Core();
            adCore1.Id = 0;
            adCore1.Title = "毕业到村里教书, 路上意外救起落水村妇, 她竟然..., 进村后惊呆了!";
            adCore1.Intro = "大学毕业被分配到欲女村教书,本来郁闷,路上却意外救起了落水少妇...";
            adCore1.ImgList = new List<string> { "http://p3tpsvuiu.bkt.clouddn.com/default/20180304201621069-731996565.png" };
            adCore1.RedirectLink = "";

            List<Core> relationListFinal = (List<Core>)articleList;
            relationListFinal.Insert(2, adCore1);

            ViewBag.RelationList = relationListFinal;

            //返回详情页面
            return View(model);
        }






    }
}