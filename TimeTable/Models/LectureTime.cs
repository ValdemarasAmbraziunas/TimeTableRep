using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   /// <summary>
    /// Paskaitos laiko klasė
    /// Apibrėžiama, kokiu laiku vyksta paskaitos 
    /// </summary>
    public class LectureTime
    {
        public int ID { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
    }
}