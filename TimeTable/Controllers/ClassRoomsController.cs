using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeTable.Data;
using TimeTable.Extensions;
using TimeTable.Models;
using TimeTable.ViewModels;

namespace TimeTable.Controllers
{
    public class ClassRoomsController : BaseController
    {
        public ClassRoomsController(ITimeTableContextTestable context)
            : base(context)
        {

        }

        public ClassRoomsController()
        {

        }

        public ActionResult Index()
        {
            return View(db.ClassRooms.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            var lectures = db.Lectures.ToList();
            var days = db.Weekdays.ToList();
            var times = db.LectureTimes.ToList();

            ViewBag.Days = days;
            ViewBag.Times = times;
            ViewBag.Lectures = lectures;
            return View(classRoom);
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem()
            {
                Text = "Teorinė",
                Value = "Teorinė"
            });
            types.Add(new SelectListItem()
            {
                Text = "Praktinė",
                Value = "Praktinė"
            });
            types.Add(new SelectListItem()
            {
                Text = "-",
                Value = "",
                Selected = true
            });

            ClassRoomViewModel model = new ClassRoomViewModel()
            {

                TypesList = types

            };
            return View(model);
        }


        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                ClassRoom classRoom = new ClassRoom()
                {
                    Name = model.Name,
                    NumberOfPlaces = model.NumberOfPlaces,
                    IsPCavailable = model.IsPCAvailable,
                    Type = model.Type

                };
                db.ClassRooms.Add(classRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            return View(classRoom);
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,NumberOfPlaces,Type,IsPCavailable")] ClassRoom classRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classRoom);
        }

        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            return View(classRoom);
        }

        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoom classRoom = db.ClassRooms.Find(id);
            db.ClassRooms.Remove(classRoom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
