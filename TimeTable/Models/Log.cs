using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    public class Log
    {
        public int ID { get; set; }
        public string LogItem { get; set; }
        public bool isUndoable { get; set; }
        public int FirstLectureID { get; set; }
        public int? SecondLectureID { get; set; }
        public int OldLectureFirstTeacherID { get; set; }
        public int? OldLectureSecondTeacherID { get; set; }
        public int OldLectureFirstClassroomID { get; set; }
        public int? OldLectureSecondClassroomID { get; set; }
        public int? OldWeekdayID { get; set; }
        public int? OldLectureTimeID { get; set; }
    }
}