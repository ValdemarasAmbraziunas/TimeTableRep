using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class WeekDaysController
    {
        [TestMethod]
        public void TestWeekDaysControllerCreate()
        {
            Controllers.WeekdaysController controller = new Controllers.WeekdaysController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
