using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   /// <summary>
    /// Akademinės grupės klasė.
    /// Nusako kokia tai grupė ir kokias paskaitas turi 
    /// </summary>
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
        //TODO: praplėsti studentų informacija
        public int StudentsCount { get; set; }
    }
}