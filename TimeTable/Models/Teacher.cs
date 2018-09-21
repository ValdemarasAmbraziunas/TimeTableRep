using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    /// <summary>
    /// Dėstytojo klasė.
    /// Informacija apie dėstytoją: vardas pavardė, kokias paskaitas veda
    /// </summary>
    public class Teacher
    {
        public int ID { get; set; }
        [Display(Name = "First name")]
        public string FirstName  { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
        //Reik priskirt bent vieną modulį
        public string Module { get; set; }
    }
}