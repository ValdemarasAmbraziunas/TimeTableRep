using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TimeTable.Data;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ITimeTableContextTestable context) 
            : base(context)
        {

        }

        public HomeController() : base() { }

        public ActionResult Index()
        {
            var lectures = db.Lectures.ToList();
            var days = db.Weekdays.ToList();
            var times = db.LectureTimes.ToList();
            
            ViewBag.Days = days;
            ViewBag.Times = times;
            ViewBag.Lectures = lectures;

            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }


    }
}