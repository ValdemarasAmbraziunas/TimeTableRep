using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TimeTable.Controllers;
using TimeTable.Models;
using System.Collections.Generic;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class GroupsControllerTest : BaseTestController
    {
        private GroupsController controller;

        public GroupsControllerTest() : base()
        {
            controller = new GroupsController(context);

            context.Groups.Add(new Group()
            {
                Name = "IFF-5/1",
                StudentsCount = 20
            });

            context.Groups.Add(new Group()
            {
                Name = "IFF-5/2",
                StudentsCount = 20
            });

            context.Groups.Add(new Group()
            {
                Name = "IFF-5/3",
                StudentsCount = 20
            });

            context.SaveChanges();
        }

        [TestMethod]
        public void TestGroupsIndexModel()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.Model as IEnumerable<TimeTable.Models.Group>);
        }

        [TestMethod]
        public void TestGroupsIndexModelCount()
        {
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model as List<Group>;
            int count = model.Count;
            Assert.AreEqual(count, 3);
        }

        [TestMethod]
        public void TestGroupsControllerCreate()
        {
            Controllers.GroupsController controler = new Controllers.GroupsController();
            ViewResult result = controler.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
