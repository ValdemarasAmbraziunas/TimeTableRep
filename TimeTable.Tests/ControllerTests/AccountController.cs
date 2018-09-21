using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class AccountController
    {
        [TestMethod]
        public void TestAccountControllerLogin()
        {
            Controllers.AccountController controler = new Controllers.AccountController();
            string url = "TEST";
            ViewResult result = controler.Login(url) as ViewResult;
           // Assert.AreEqual(url, result.ViewBag.ReturnUrl); // ERROR?
        }

        [TestMethod]
        public void TestAccountControllerRegister()
        {
            Controllers.AccountController controler = new Controllers.AccountController();
            ViewResult result = controler.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAccountControllerResetPassword()
        {
            Controllers.AccountController controler = new Controllers.AccountController();
            string code1 = "TEST";
            string code2 = null;
            ViewResult result1 = controler.ResetPassword(code1) as ViewResult;
            ViewResult result2 = controler.ResetPassword(code2) as ViewResult;
            Assert.IsNotNull(result1);
            Assert.AreEqual("Error",result2.ViewName);
        }

        public void TestAccountControllerLogOff()
        {
            Controllers.AccountController controller = new Controllers.AccountController();
            ActionResult result = controller.LogOff() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }
    }
}
