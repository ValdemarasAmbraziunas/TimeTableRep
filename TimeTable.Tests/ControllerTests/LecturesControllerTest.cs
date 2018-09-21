using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TimeTable.Controllers;
using TimeTable.Models;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable.Tests.ControllerTests
{
    [TestClass]
    public class LecturesControllerTest : BaseTestController
    {
        private Random r;
        private LecturesController controller;
        //private List<Lecture> lectureList;

        public LecturesControllerTest() : base()
        {
            controller = new LecturesController(context);

            //GRUPES
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

            //AUDITORIJOS
            context.ClassRooms.Add(new ClassRoom()
            {
                IsPCavailable = true,
                Name = "101",
                NumberOfPlaces = 30,
                Type = "Praktine"
            });

            context.ClassRooms.Add(new ClassRoom()
            {
                IsPCavailable = true,
                Name = "102",
                NumberOfPlaces = 30,
                Type = "Praktine"
            });

            context.ClassRooms.Add(new ClassRoom()
            {
                IsPCavailable = true,
                Name = "103",
                NumberOfPlaces = 30,
                Type = "Praktine"
            });

            //MOKYTOJAI
            context.Teachers.Add(new Teacher()
            {
                FirstName="Juozas",
                LastName="Zuokas",
                Module="Matematika"
            });

            context.Teachers.Add(new Teacher()
            {
                FirstName = "Petras",
                LastName = "Mazeikis",
                Module = "Fizika"
            });

            context.Teachers.Add(new Teacher()
            {
                FirstName = "Arnas",
                LastName = "Gelezinis",
                Module = "Programavimas"
            });

            //LAIKAI
            context.LectureTimes.Add(new LectureTime()
            {
                Start = new TimeSpan(10, 30, 0),
                End = new TimeSpan(12, 0, 0)
            });

            context.LectureTimes.Add(new LectureTime()
            {
                Start = new TimeSpan(13, 0, 0),
                End = new TimeSpan(14, 30, 0)
            });

            context.LectureTimes.Add(new LectureTime()
            {
                Start = new TimeSpan(15, 0, 0),
                End = new TimeSpan(16, 30, 0)
            });

            //DIENOS
            context.Weekdays.Add(new Weekday()
            {
                Name = "Pirmadienis"
            });

            context.Weekdays.Add(new Weekday()
            {
                Name = "Antradienis"
            });

            context.Weekdays.Add(new Weekday()
            {
                Name = "Treciadienis"
            });

            //MODULIAI
            context.Subjects.Add(new Subject()
            {
                Name = "Matematika",
                Code = "123"
            });

            context.Subjects.Add(new Subject()
            {
                Name = "Fizika",
                Code = "456"
            });

            context.Subjects.Add(new Subject()
            {
                Name = "Programavimas",
                Code = "789"
            });

            List<Teacher> teacherList = context.Teachers.ToList();
            List<Group> groupList = context.Groups.ToList();
            List<LectureTime> lectureTimeList = context.LectureTimes.ToList();
            List<ClassRoom> classRoomsList = context.ClassRooms.ToList();
            List<Weekday> weekDayList = context.Weekdays.ToList();
            List<Subject> subjectList = context.Subjects.ToList();

            context.Lectures.Add(new Lecture()
            {
                TeacherID = teacherList[0].ID,
                GroupID = groupList[0].ID,
                LectureTimeID = lectureTimeList[0].ID,
                ClassRoomID = classRoomsList[0].ID,
                WeekdayID = weekDayList[0].ID,
                SubjectID = subjectList[0].ID,
                Type = "Praktine",
                IsPcRequired = false
            });
            context.Lectures.Add(new Lecture()
            {
                TeacherID = teacherList[1].ID,
                GroupID = groupList[1].ID,
                LectureTimeID = lectureTimeList[1].ID,
                ClassRoomID = classRoomsList[1].ID,
                WeekdayID = weekDayList[1].ID,
                SubjectID = subjectList[1].ID,
                Type = "Praktine",
                IsPcRequired = false
            });
            context.Lectures.Add(new Lecture()
            {
                TeacherID = teacherList[2].ID,
                GroupID = groupList[2].ID,
                LectureTimeID = lectureTimeList[2].ID,
                ClassRoomID = classRoomsList[2].ID,
                WeekdayID = weekDayList[2].ID,
                SubjectID = subjectList[2].ID,
                Type = "Praktine",
                IsPcRequired = false
            });
            context.Lectures.Add(new Lecture()
            {
                TeacherID = teacherList[0].ID,
                GroupID = groupList[0].ID,
                LectureTimeID = lectureTimeList[1].ID,
                ClassRoomID = classRoomsList[1].ID,
                WeekdayID = weekDayList[2].ID,
                SubjectID = subjectList[0].ID,
                Type = "Praktine",
                IsPcRequired = false
            });
            context.Lectures.Add(new Lecture()
            {
                TeacherID = teacherList[0].ID,
                GroupID = groupList[0].ID,
                LectureTimeID = lectureTimeList[2].ID,
                ClassRoomID = classRoomsList[1].ID,
                WeekdayID = weekDayList[1].ID,
                SubjectID = subjectList[0].ID,
                Type = "Praktine",
                IsPcRequired = false
            });

            List<Lecture> lectureList = context.Lectures.ToList();

            context.SaveChanges();
        }

        [TestMethod]
        public void TestLecturesControllerIndex()
        {
            Controllers.LecturesController controller = new Controllers.LecturesController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        //[TestMethod]
        //public void TestLecturesControllerSwap()
        //{
        //    int I1 = 0;
        //    int I2 = 0;

        //    for (int i = 0; i <= 1; i++)
        //    {
        //        while(I1 == I2)
        //        {
        //            I1 = r.Next(0, 5);
        //            I2 = r.Next(0, 5);
        //        }
        //        ActionResult result = controller.Swap2Lectures(lectureList[I1].ID, lectureList[I2].ID) as ActionResult;
        //        Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //        RedirectToRouteResult routeResult = result as RedirectToRouteResult;
        //        Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        //        I1 = 0;
        //        I2 = 0;
        //    }
        //}

        [TestMethod]
        public void TestLecturesControllerGenerateTable()
        {
            Controllers.LecturesController controller = new Controllers.LecturesController();
            ActionResult result = controller.GenerateTable() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }
    }
}
