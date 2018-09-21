using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using TimeTable.Models;
using System.Configuration;

namespace TimeTable.Data
{
    public class TimeTableContext : DbContext, ITimeTableContextTestable
    {
        public TimeTableContext() : base("DefaultConnection") { }

        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureTime> LectureTimes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Weekday> Weekdays { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //builder.Entity<Lecture>().HasOptional(x => x.LectureTime).WithOptionalPrincipal().Map(x => x.MapKey("LectureTimeID"));
            //builder.Entity<Lecture>().HasOptional(x => x.Weekday).WithOptionalPrincipal().Map(x => x.MapKey("WeekdayID"));

            //builder.Entity<Group>().HasMany(x => x.Lectures).WithRequired(x => x.Group);
            //builder.Entity<Lecture>().HasRequired(x => x.Group).WithMany(x => x.Lectures);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    //builder.UseSqlServer(ConfigurationManager.ConnectionStrings["TimeTableContext"].ConnectionString);
        //}
    }
}