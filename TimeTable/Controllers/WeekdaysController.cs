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
    public class WeekdaysController : BaseController
    {

        // GET: Weekdays
        public ActionResult Index()
        {
            return View(db.Weekdays.ToList());
        }

        // GET: Weekdays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekday weekday = db.Weekdays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Weekday weekday)
        {
            if (ModelState.IsValid)
            {
                db.Weekdays.Add(weekday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weekday);
        }

        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekday weekday = db.Weekdays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Weekday weekday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weekday);
        }

        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekday weekday = db.Weekdays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weekday weekday = db.Weekdays.Find(id);
            db.Weekdays.Remove(weekday);
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
