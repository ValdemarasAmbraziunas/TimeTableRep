using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Data;

namespace TimeTable.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ITimeTableContextTestable db;

        public BaseController(ITimeTableContextTestable context) : base()
        {
            db = context;
        }

        public BaseController() : base()
        {
            db = new TimeTableContext();
        }
    }
}