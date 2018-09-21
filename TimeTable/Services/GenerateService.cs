using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Data;
using TimeTable.Models;

namespace TimeTable.Services
{
    public class GenerateService : IGenerateService
    {
        protected TimeTableContext db;

        public GenerateService() : base()
        {
            db = new TimeTableContext();
        }


        /// <summary>
        /// Paskaitu laiko priskirimo metodas
        /// </summary>
        public void Generate()
        {
            //Visi galimi laikai
            List<LectureTime> lecturesTimes = db.LectureTimes.ToList();
            //Visos galimso dienos
            List<Weekday> weekdays = db.Weekdays.ToList();
            //Visos klasės su tvarkaraščiais
            List<ClassRoom> classrooms = db.ClassRooms.ToList();
            List<TimeSchedule> classSchedule = new List<TimeSchedule>();
            foreach (ClassRoom item in classrooms)
            {
                TimeSchedule schedule = new TimeSchedule(item.ID, lecturesTimes.Count);
                classSchedule.Add(schedule);
            }
            //Visi dėstytojai su tvarkaraščiais
            List<Teacher> teachers = db.Teachers.ToList();
            List<TimeSchedule> teachersSchedule = new List<TimeSchedule>();
            foreach (Teacher item in teachers)
            {
                TimeSchedule schedule = new TimeSchedule(item.ID, lecturesTimes.Count);
                teachersSchedule.Add(schedule);
            }
            //Visos grupės su tvarkaraščiais
            List<Group> groups = db.Groups.ToList();
            List<TimeSchedule> groupsSchedule = new List<TimeSchedule>();
            foreach (Group item in groups)
            {
                TimeSchedule schedule = new TimeSchedule(item.ID, lecturesTimes.Count);
                groupsSchedule.Add(schedule);
            }

            foreach (Lecture lecture in db.Lectures)
            {
                //Išrenkam reikiamus dalykus
                Teacher currTeacher = lecture.Teacher;
                Group currGroup = lecture.Group;
                ClassRoom currClass = lecture.ClassRoom;
                TimeSchedule currTeacherSched = teachersSchedule.Where(x => x.ID == currTeacher.ID).Select(x => x).FirstOrDefault();
                TimeSchedule currGroupChed = groupsSchedule.Where(x => x.ID == currGroup.ID).Select(x => x).FirstOrDefault();
                TimeSchedule currClassSched = classSchedule.Where(x => x.ID == currClass.ID).Select(x => x).FirstOrDefault();
                //Jei nėra laiko, ieškom laisvo laiko
                if (lecture.LectureTimeID == null && lecture.WeekdayID == null)
                {
                    foreach (Weekday day in weekdays)
                    {
                        foreach (LectureTime time in lecturesTimes)
                        {
                            //Patikrinam ar šiuo laiku dėstytojas,grupė ir auditorija yra laisva
                            if (currTeacherSched.isFree(day.ID, time.ID) && currGroupChed.isFree(day.ID, time.ID) && currClassSched.isFree(day.ID, time.ID))
                            {
                                lecture.LectureTimeID = time.ID;
                                lecture.WeekdayID = day.ID;
                                currTeacherSched.setTrue(day.ID, time.ID);
                                currGroupChed.setTrue(day.ID, time.ID);
                                currClassSched.setTrue(day.ID, time.ID);
                                break;
                            }
                        }
                        //Patirkinam ar radom laiką paskaitai
                        if (lecture.LectureTimeID != null && lecture.WeekdayID != null)
                        {
                            break;
                        }
                    }
                }
                //Jei laikas priskirtas pasižymime į tvarkaraščius
                else
                {
                    currTeacherSched.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                    currGroupChed.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                    currClassSched.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                }

            }
            db.SaveChanges();
        }

        /// <summary>
        /// Paprasta validacija duombazes duomenu
        /// </summary>
        public void Validate()
        {
            var rand = new Random();
            List<Lecture> lectures = db.Lectures.ToList();
            foreach (Lecture var in lectures)
            {
                    if (var.WeekdayID == null)
                    {
                        Teacher teacher = var.Teacher;
                        ClassRoom classroom = var.ClassRoom;
                        if (teacher.Module != var.Subject.Name)
                        {
                            Teacher[] arrayT = db.Teachers.Where(x => x.Module == var.Subject.Name).Select(x => x).ToArray();
                            if (arrayT.Length > 0)
                            {
                                Teacher newT = arrayT.ElementAt(rand.Next(0, arrayT.Length));
                                var.Teacher = newT;
                                var.TeacherID = newT.ID;
                            }
                            else
                                db.Lectures.Remove(var);
                        }
                        if (classroom.Type != var.Type || classroom.NumberOfPlaces < var.Group.StudentsCount || classroom.IsPCavailable != var.IsPcRequired)
                        {
                            ClassRoom[] arrayC = db.ClassRooms.Where(x => x.Type == var.Type && x.IsPCavailable == var.IsPcRequired && x.NumberOfPlaces >= var.Group.StudentsCount).Select(x => x).ToArray();
                            if (arrayC.Length > 0)
                            {
                                ClassRoom newC = arrayC.ElementAt(rand.Next(0, arrayC.Length));
                                var.ClassRoom = newC;
                                var.ClassRoomID = newC.ID;
                            }
                            else
                                db.Lectures.Remove(var);
                        }
                    }
            }
            db.SaveChanges();
        }
    }
}