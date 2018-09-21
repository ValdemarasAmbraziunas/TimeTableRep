using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class SubjectsController
    {
        [TestMethod]
        public void TestSubjectsControllerCreate()
        {
            Controllers.SubjectsController controller = new Controllers.SubjectsController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
