using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTable.Models;
using TimeTable.Data;
using System.Data.Entity;

namespace TimeTable.Tests
{
    class TimeTableContextDummy : DbContext, ITimeTableContextTestable
    {
        public TimeTableContextDummy() : base("TestConnection")
        {
        }

        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureTime> LectureTimes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Weekday> Weekdays { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
