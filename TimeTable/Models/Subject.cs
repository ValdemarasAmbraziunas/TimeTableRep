using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   
    /// <summary>
    /// Studijų modulio klasė, nurodomas jo pavadinimas ir dėstytojai, kurie veda
    /// </summary>
    public class Subject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
        //Todo pridėti modulio kodą
        public string Code { get; set; }
    }
}