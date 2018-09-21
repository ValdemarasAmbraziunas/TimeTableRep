using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{   
    /// <summary>
    /// Auditorijos klasė.
    /// Nusako kokia auditoja, kur ji yra ir kokios paskaitos vyksta joje
    /// </summary>
    public class ClassRoom
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
        //Todo: praplėsti adresu ir tipu.
        public int NumberOfPlaces { get; set; }
        public string Type { get; set; }
        public bool IsPCavailable { get; set; }
    }
}