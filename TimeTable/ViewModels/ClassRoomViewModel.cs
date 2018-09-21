using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Models;

namespace TimeTable.ViewModels
{
    public class ClassRoomViewModel
    {
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Number of places")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Number of places is required.")]
        public int NumberOfPlaces { get; set; }

        [Display(Name = "Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        public List<SelectListItem> TypesList { get; set; }
        [Display(Name = "IsPCAvailable")]
        public bool IsPCAvailable { get; set; }
    }
}