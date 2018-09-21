using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    /// <summary>
    /// Tvarkaraščio klasė
    /// Klasė, kuri paskaitas sudeda į matricą
    /// </summary>
    public class TimeSchedule
    {
        public int ID { get; set; }
        /// <summary>
        /// Tvarkaraščio matrica
        /// </summary>
        public bool[,] Schedule { get; set; }
        /// <summary>
        /// Dienų skaičius per savaitę kada vyksta paskaitos
        /// </summary>
        public int DaysCount = 5;
        /// <summary>
        /// Paskaitų laikų skaičius per dieną
        /// </summary>
        public int TimesCount { get; set; }
        public TimeSchedule(int id, int tcount)
        {
            this.ID= id;
            this.TimesCount = tcount;
            this.Schedule = new bool[DaysCount, TimesCount];
            
        }

        /// <summary>
        /// Nustato ar vieta matricoj užimta
        /// </summary>
        /// <param name="i">matricos eilute</param>
        /// <param name="j">matricos stulpelis</param>
        /// <returns>False - neuzimta, true - uzimta</returns>
        public bool isFree(int i, int j)
        {
            //revertinam, kad kai reik ir false yra neuzimta grazintu true
            return !Schedule[i - 1, j - 1];
        }
        /// <summary>
        /// Užpildo vietą matricoje
        /// </summary>
        /// <param name="i">matricos eilutė</param>
        /// <param name="j">matricos stulpelis</param>
        public void setTrue(int i, int j)
        {
            Schedule[i - 1, j - 1] = true;
        }

        /// <summary>
        /// Atlaisvina vietą matricoje
        /// </summary>
        /// <param name="i">matricos eilutė</param>
        /// <param name="j">matricos stulpelis</param>
        public void setFalse(int i, int j)
        {
            Schedule[i - 1, j - 1] = false;
        }
    }
}