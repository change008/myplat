using log4net;
using myplat.Biz;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace myplat.UIWap.Controllers
{
    /// <summary>
    /// App API接口控制器 全在这里
    /// </summary>
    public partial class TTApiController : Controller
    {

        #region -- 类属性定义 --

        //文章业务类
        private static CoreBiz _CoreBiz = new CoreBiz();


        /// <summary>
        /// log4net
        /// </summary>
        private static ILog _logger = LogManager.GetLogger(typeof(TTApiController));
        #endregion



    }
}
