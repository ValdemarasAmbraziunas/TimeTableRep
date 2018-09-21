using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using System.Data.Entity.Infrastructure;

namespace TimeTable.Data
{
    public interface ITimeTableContextTestable
    {
        DbSet<ClassRoom> ClassRooms { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Lecture> Lectures { get; set; }
        DbSet<LectureTime> LectureTimes { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Weekday> Weekdays { get; set; }
        DbSet<Log> Logs { get; set; }


        void Dispose();
        int SaveChanges();
        DbEntityEntry Entry(object entity);

    }
}