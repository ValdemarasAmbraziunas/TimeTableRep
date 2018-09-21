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

namespace TimeTable.Controllers
{
    public class LectureTimesController : BaseController
    {

        // GET: LectureTimes
        public ActionResult Index()
        {
            return View(db.LectureTimes.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LectureTime lectureTime = db.LectureTimes.Find(id);
            if (lectureTime == null)
            {
                return HttpNotFound();
            }
            return View(lectureTime);
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Start,End")] LectureTime lectureTime)
        {
            if (ModelState.IsValid)
            {
                db.LectureTimes.Add(lectureTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lectureTime);
        }

        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LectureTime lectureTime = db.LectureTimes.Find(id);
            if (lectureTime == null)
            {
                return HttpNotFound();
            }
            return View(lectureTime);
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Start,End")] LectureTime lectureTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lectureTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lectureTime);
        }

        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LectureTime lectureTime = db.LectureTimes.Find(id);
            if (lectureTime == null)
            {
                return HttpNotFound();
            }
            return View(lectureTime);
        }

        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LectureTime lectureTime = db.LectureTimes.Find(id);
            db.LectureTimes.Remove(lectureTime);
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
