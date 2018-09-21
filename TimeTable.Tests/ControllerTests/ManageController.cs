using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class ManageController
    {
        [TestMethod]
        public void TestManageControllerAddPhoneNumber()
        {
            Controllers.ManageController controller = new Controllers.ManageController();
            ViewResult result = controller.AddPhoneNumber() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestManageControllerChangePasswordr()
        {
            Controllers.ManageController controller = new Controllers.ManageController();
            ViewResult result = controller.ChangePassword() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestManageControllerSetPassword()
        {
            Controllers.ManageController controller = new Controllers.ManageController();
            ViewResult result = controller.SetPassword() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
