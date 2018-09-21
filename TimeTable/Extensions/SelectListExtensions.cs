using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTable.Extensions
{
    public static class SelectListExtensions
    {
        public static List<SelectListItem> AddDefaultItem(this List<SelectListItem> list)
        {
            list.Insert(0, new SelectListItem() { Value = "0", Text = "-", Selected = true });
            return list;
        }
    }
}