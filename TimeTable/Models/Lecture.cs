using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   
    /// <summary>
    /// Paskaitos klasė
    /// Nusako konkrečios paskaitos informamciją: kur kokiu laiku koks modulis vyksta,
    /// kas jį vedą, kokie studentai klauso, 
    /// </summary>
    public class Lecture
    {
        public int ID { get; set; }
        
        public int TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }
        
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        public int ClassRoomID { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }
        public int? LectureTimeID { get; set; }
        public virtual LectureTime LectureTime { get; set; }
        public int? WeekdayID { get; set; }
        public virtual Weekday Weekday { get; set; }
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }
        public string Type { get; set; }
        public bool IsPcRequired { get; set; }

        public override string ToString()
        {
            return "ID: " + ID +" "+ "Subject: " + Subject.Name + " " +"Day: " + Weekday.Name + " " + "Start time: " + LectureTime.Start + " "  + "Teacher: " + Teacher.LastName + " " + Teacher.FirstName;
        }
    }
}