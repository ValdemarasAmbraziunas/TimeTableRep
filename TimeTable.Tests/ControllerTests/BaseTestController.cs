using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTable.Controllers;
using TimeTable.Data;

namespace TimeTable.Tests.ControllerTests
{
    public abstract class BaseTestController : IDisposable
    {
        protected readonly ITimeTableContextTestable context;
        
        public BaseTestController()
        {
            context = new TimeTableContextDummy();
        }

        public void Dispose()
        {
            foreach (var obj in context.ClassRooms)
            {
                context.ClassRooms.Remove(obj);
            }

            foreach (var obj in context.Groups)
            {
                context.Groups.Remove(obj);
            }

            foreach (var obj in context.Lectures)
            {
                context.Lectures.Remove(obj);
            }

            foreach (var obj in context.LectureTimes)
            {
                context.LectureTimes.Remove(obj);
            }

            foreach (var obj in context.Logs)
            {
                context.Logs.Remove(obj);
            }

            foreach (var obj in context.Subjects)
            {
                context.Subjects.Remove(obj);
            }

            foreach (var obj in context.Teachers)
            {
                context.Teachers.Remove(obj);
            }

            foreach (var obj in context.Weekdays)
            {
                context.Weekdays.Remove(obj);
            }

            context.SaveChanges();
            context.Dispose();
        }
    }
}
