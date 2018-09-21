using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using TimeTable.Controllers;
using TimeTable.Models;
using TimeTable.Data;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class ClassRoomsControllerTest : BaseTestController
    {
        private ClassRoomsController controller;

        //kiekvienoj implementuojancioj klasej prisidedam duomenis
        //per kiekviena testu paleidima jie is naujo sukuriami ir poto panaikinami

        //atrkreipt demesi kad ID kiekviena karta didesnis, t..y niekada nebus tas pats,
        //todel juo geriau nepasitiket
        public ClassRoomsControllerTest() : base()
        {
            controller = new ClassRoomsController(context);

            context.ClassRooms.Add(new ClassRoom()
            {
                IsPCavailable = false,
                Name = "101",
                NumberOfPlaces = 27,
                Type = "Teorine"
            });

            context.ClassRooms.Add(new ClassRoom()
            {
                IsPCavailable = true,
                Name = "101",
                NumberOfPlaces = 69,
                Type = "Praktine"
            });
            
            context.SaveChanges();
        }

        [TestMethod]
        public void TestIndexModel()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.Model as IEnumerable<TimeTable.Models.ClassRoom>);
        }

        [TestMethod]
        public void TestIndexModelCount()
        {
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model as List<ClassRoom>;
            int count = model.Count;
            //kadangi konstruktoriuje pridejom du irasus, tai tiek ir turi but
            Assert.AreEqual(count, 2);
        }

        [TestMethod]
        public void TestEditResponse()
        {
            //paimam pati pirma is duomabazes
            ClassRoom firstObject = context.ClassRooms.FirstOrDefault();
            //jei nepavyksta kazkodel tai failina toliau testai
            Assert.IsNotNull(firstObject);

            int id = firstObject.ID;

            //ar geras response
            ViewResult result = controller.Edit(id) as ViewResult;
            Assert.IsNotNull(result);

            //ar geras modelis
            var model = result.Model as ClassRoom;
            Assert.IsNotNull(model);
        }
    }
}
