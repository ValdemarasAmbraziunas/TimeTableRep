using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeTable.Data;
using TimeTable.Models;
using TimeTable.ViewModels;
using TimeTable.Extensions;
using TimeTable.Services;
using System.Text;

namespace TimeTable.Controllers
{

    public class LecturesController : BaseController
    {
        private readonly ISwapService swapService;
        private readonly IGenerateService generateService;
        
        public LecturesController(ISwapService swapService, IGenerateService generateService)
        {
            this.swapService = swapService;
            this.generateService = generateService;
        }

        public LecturesController(ITimeTableContextTestable context)
            : base(context)
        {

        }

        public LecturesController() : base()
        {
        }

        public ActionResult Index()
        {
            return View(db.Lectures.ToList());
        }
        /// <summary>
        /// Metodas sugeneruojantis tvarkaraštį
        /// </summary>
        /// <returns>Lecture indexview su laikais</returns>
        [HttpPost]
        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateTable()
        {
            try
            {
                generateService.Validate();
                generateService.Generate();
                return RedirectToAction("Index");
            }
            catch (NullReferenceException e)
            {
                ViewBag.Error = "Duombazėje nėra tokių indeksų: " + e.Source;

                return RedirectToAction("Index", "Home");
            }
        }
        /// <summary>
        /// Metodas, kuris apkeičia dviejų paskaitų laikus, jeigu tai yra įmanoma
        /// </summary>
        /// <param name="ID1">Pirmos Lecture ID</param>
        /// <param name="ID2">Antros Lecture ID</param>
        /// <returns>Lecture index view</returns>
        [HttpPost]
        [AdminAuthorize]

        public ActionResult Swap2Lectures(int ID1, int ID2)
        {
            try
            {
                Log log = new Log();
                string msg = "";
                Lecture first = db.Lectures.Find(ID1);
                log.OldLectureFirstTeacherID = first.TeacherID;
                log.OldLectureFirstClassroomID = first.ClassRoomID;
                Lecture second = db.Lectures.Find(ID2);
                log.OldLectureSecondTeacherID = second.TeacherID;
                log.OldLectureSecondClassroomID = second.ClassRoomID;
                string info = first.ToString();
                string info2 = second.ToString();
                swapService.Swap2Lectures(ID1, ID2, ref msg);
                if (msg.Contains("successful"))
                {
                    log.isUndoable = true;
                }
                else log.isUndoable = false;
                log.LogItem = string.Format("Lecture1: {0}\n Lecture 2: {1}\n Message: {2}\n", info, info2, msg);
                log.FirstLectureID = ID1;
                log.SecondLectureID = ID2;
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (NullReferenceException e)
            {
                ViewBag.Error = "Duombazėje nėra tokių indeksų: " + e.Source;

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Undo(int logItemID)
        {
            try
            {
                swapService.Undo(logItemID);
                return RedirectToAction("Index", "Home");
            }
            catch (NullReferenceException e)
            {
                ViewBag.Error = "Duombazėje nėra tokio log indekso: " + e.Source;

                return RedirectToAction("Index", "Home");
            }
        }

        [AdminAuthorize]
        public ActionResult GetLog()
        {
            int count = db.Logs.Count();
            if (count > 10)
                return Json(db.Logs.OrderBy(x => x.ID).Select(x => new { x.LogItem, x.ID, x.isUndoable }).Skip(count - 10).Take(10).ToArray().Reverse(), JsonRequestBehavior.AllowGet);
            else
                return Json(db.Logs.OrderBy(x => x.ID).Select(x => new { x.LogItem, x.ID, x.isUndoable }).ToArray().Reverse(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult GetAvailableChanges(int lectureToSwapID, bool swapTeacher, bool swapClassroom)
        {
            try
            {
                List<Object> list = swapService.FindPossibleChanges(lectureToSwapID, swapTeacher, swapClassroom);
                return Json(list, JsonRequestBehavior.AllowGet);
    
            }
            catch (NullReferenceException e)
            {
                ViewBag.Error = "Duombazėje nėra tokio log indekso: " + e.Source;

                return RedirectToAction("Index", "Home");
            }
        }


        /// <summary>
        /// Metodas priskiriantis paskaitai naują laiką
        /// </summary>
        /// <param name="lectureId">Lecture ID</param>
        /// <param name="newLectureTimeId">Naujo LectureTime ID</param>
        /// <param name="newLectureDayId">Naujo Weeday ID</param>
        /// <returns>Lecture index view</returns>
        [HttpPost]
        [AdminAuthorize]
        public ActionResult ChangeLectureTime(int lectureId, int newLectureTimeId, int newLectureDayId)
        {
            try
            {
                Log log = new Log();
                Lecture first = db.Lectures.Find(lectureId);
                log.OldLectureFirstTeacherID = first.TeacherID;
                log.OldLectureFirstClassroomID = first.ClassRoomID;
                log.OldLectureTimeID = first.LectureTimeID;
                log.OldWeekdayID = first.WeekdayID;
                string info = first.ToString();
                string msg = "";
                swapService.ChangeLectureTime(lectureId, newLectureTimeId, newLectureDayId, ref msg);
                if (msg.Contains("successful"))
                {
                    log.isUndoable = true;
                }
                else log.isUndoable = false;
                log.LogItem = string.Format("Lecture: {0} New day: {1} New start time: {2} Message: {3}", info, db.Weekdays.Where(x => x.ID == newLectureDayId).Select(x => x.Name).FirstOrDefault(), db.LectureTimes.Where(x => x.ID == newLectureTimeId).Select(x => x.Start).FirstOrDefault(), msg);
                log.FirstLectureID = lectureId;
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (NullReferenceException e)
            {
                ViewBag.Error = "Duombazėje nėra tokių indeksų: " + e.Source;

                return RedirectToAction("Index", "Home");
            }
        }



        /// <summary>
        /// Grazina informacija apie duombazės įrašą
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Informacija apie irasa</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        /// <summary>
        /// Sukuria irasa duombazėje
        /// </summary>
        /// <returns>View modeli</returns>
        /// [AdminAuthorize]
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

            LectureViewModel model = new LectureViewModel()
            {
                Teachers = db.Teachers.ToList().Select(x => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", x.FirstName, x.LastName),
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                ClassRooms = db.ClassRooms.ToList().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                Groups = db.Groups.ToList().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                Subjects = db.Subjects.ToList().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                LectureTimes = db.LectureTimes.ToList().Select(x => new SelectListItem()
                {
                    Text = string.Format("{0} - {1}", x.Start, x.End),
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                Weekdays = db.Weekdays.ToList().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }).ToList().AddDefaultItem(),

                TypesList = types
            };

            return View(model);
        }

        /// <summary>
        /// Sukuria atributus irasui
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View modeli</returns>
        [HttpPost]
        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LectureViewModel model)
        {
            if (ModelState.IsValid)
            {
                Lecture lecture = new Lecture()
                {
                    ClassRoomID = model.ClassRoomID,
                    GroupID = model.GroupID,
                    LectureTimeID = model.LectureTimeID == 0 ? null : model.LectureTimeID,
                    SubjectID = model.SubjectID,
                    TeacherID = model.TeacherID,
                    WeekdayID = model.WeekdayID == 0 ? null : model.WeekdayID,
                    Type = model.Type,
                    IsPcRequired = model.IsPCRequired
                };

                db.Lectures.Add(lecture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        /// <summary>
        /// Leidžia pakeisti Lecture įrašo duomenis
        /// </summary>
        /// <param name="id">Įrašo id</param>
        /// <returns>View lecture</returns>
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeacherID,GroupID,ClassRoomID,LectureTimeID,WeekdayID,SubjectID,Type,IsPcRequired")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }
        /// <summary>
        /// Istrian lecture įrašą
        /// </summary>
        /// <param name="id">Lecture įrašo id</param>
        /// <returns>Lecture index view</returns>
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }
        /// <summary>
        /// Ištrina įrašą iš duombazės
        /// </summary>
        /// <param name="id">Trinamos lecture id</param>
        /// <returns>Lecture index view modeli</returns>
        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lecture lecture = db.Lectures.Find(id);
            db.Lectures.Remove(lecture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Sunaikina duombazę
        /// </summary>
        /// <param name="disposing">true or false ar naikinti?</param>
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
