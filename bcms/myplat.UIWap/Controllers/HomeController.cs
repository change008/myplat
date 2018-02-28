using log4net;
using myplat.Biz;
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
            var model = _CoreBiz.Get(id);
            if (model == null)
            {
                Response.RedirectToRoute(new { controller = "Home", action = "Index" });
                return View(model);
            }

            //获取更多相关文章逻辑
            //var articleList = _CoreBiz.GetMoreArticleListById(id, 1, RowNumber);

            //返回详情页面
            return View(model);
        }


    }
}