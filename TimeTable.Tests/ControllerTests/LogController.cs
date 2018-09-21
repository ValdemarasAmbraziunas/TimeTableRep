using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class LogController
    {
        [TestMethod]
        public void TestLogControllerCreate()
        {
            Controllers.LogController controller = new Controllers.LogController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLogControllerDeleteConfirmed()
        {
            
            Controllers.LogController controller = new Controllers.LogController();
            int ID = 2;
            ActionResult result = controller.DeleteConfirmed(ID) as ActionResult; // null value metode!??
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }
    }
}
