using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Wordbook.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        protected static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            ViewData["Message"] = "ASP.NET MVC へようこそ";

            logger.Info("test log");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
