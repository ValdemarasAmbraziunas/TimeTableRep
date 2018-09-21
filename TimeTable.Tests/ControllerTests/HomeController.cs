using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using TimeTable.Controllers;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class HomeController
    {
        [TestMethod]
        public void TestHomeControllerIndex()
        {
            Controllers.HomeController controller = new Controllers.HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestHomeControllerUnauthorized()
        {
            Controllers.HomeController controller = new Controllers.HomeController();
            ViewResult result = controller.Unauthorized() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
