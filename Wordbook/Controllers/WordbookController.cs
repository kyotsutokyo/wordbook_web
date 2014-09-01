using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Wordbook.Service;

namespace Wordbook.Controllers
{
    [HandleError]
    public class WordbookController : Controller
    {
        protected static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            string sess= Session.SessionID;
            bool isLogin = SessionUtils.CheckSession(Session, HttpContext.Session.SessionID);
            if (isLogin)
            {
                Uri uri = Request.UrlReferrer;
                ViewData["Message"] = "ASP.NET MVC へようこそ";

                logger.Info("test log");
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Login()
        {
            bool isLogin = SessionUtils.CheckSession(Session, HttpContext.Session.SessionID);
            if (isLogin)
            {
                return View("Index");
            }
            //Uri uri = Request.UrlReferrer;
            ViewData["Message"] = "ASP.NET MVC へようこそ";
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
