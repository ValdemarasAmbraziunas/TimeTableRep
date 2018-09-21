using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Models;

namespace TimeTable.ViewModels
{
    public class LectureViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Teacher")]
        [Range(1, int.MaxValue, ErrorMessage = "Teacher is required.")]
        public int TeacherID { get; set; }
        //public Teacher Teacher { get; set; }
        public List<SelectListItem> Teachers { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Group is required.")]
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        public List<SelectListItem> Groups { get; set; }
        [Display(Name = "Class room")]
        [Range(1, int.MaxValue, ErrorMessage = "Class room is required.")]
        public int ClassRoomID { get; set; }
        public List<SelectListItem> ClassRooms { get; set; }
        [Display(Name = "Lecture time")]
        public int? LectureTimeID { get; set; }
        public List<SelectListItem> LectureTimes { get; set; }
        [Display(Name = "Weekday")]
        public int? WeekdayID { get; set; }
        public List<SelectListItem> Weekdays { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Subject is required.")]
        [Display(Name = "Subject")]
        public int SubjectID { get; set; }
        public List<SelectListItem> Subjects { get; set; }

        [Display(Name = "Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        public List<SelectListItem> TypesList { get; set; }

        [Display(Name = "IsPCRequired")]
        public bool IsPCRequired { get; set; }
    }
}