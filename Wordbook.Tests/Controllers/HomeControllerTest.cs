using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wordbook;
using Wordbook.Controllers;

namespace Wordbook.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // 準備
            HomeController controller = new HomeController();

            // 実行
            ViewResult result = controller.Index() as ViewResult;

            // アサート
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("ASP.NET MVC へようこそ", viewData["Message"]);
        }

        [TestMethod]
        public void About()
        {
            // 準備
            HomeController controller = new HomeController();

            // 実行
            ViewResult result = controller.About() as ViewResult;

            // アサート
            Assert.IsNotNull(result);
        }
    }
}
