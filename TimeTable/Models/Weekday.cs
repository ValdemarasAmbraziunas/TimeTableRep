using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   
    /// <summary>
    /// Savaitės dienos klasė
    /// Nusako, kuri diena ir kokios paskaitos vyksta tą dieną
    /// </summary>
    public class Weekday
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }

    }
}