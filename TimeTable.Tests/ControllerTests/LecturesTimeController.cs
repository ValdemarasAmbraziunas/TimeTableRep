using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class LecturesTimeController
    {
        [TestMethod]
        public void TestLecturesTimeControllerCreate()
        {
            Controllers.LectureTimesController controller = new Controllers.LectureTimesController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
