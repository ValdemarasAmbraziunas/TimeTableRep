using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Models;

namespace TimeTable.ViewModels
{
    public class TeacherViewModel
    {

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Subject is required.")]
        [Display(Name = "Subject")]
        public int SubjectID { get; set; }
        public List<SelectListItem> Subjects { get; set; }
    }
}