using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using TimeTable.Controllers;
using TimeTable.Data;

namespace TimeTable.Services
{
    public class SwapService : ISwapService
    {
        protected TimeTableContext db;

        public SwapService() : base()
        {
            db = new TimeTableContext();
        }
        /// <summary>
        /// Apkeicia dvi paskaitas vietomis, jei tai imanoma
        /// </summary>
        /// <param name="ID1">Pirmos paskaitos id</param>
        /// <param name="ID2">Antros paskaitod id</param>
        /// <param name="msg">Pranesimas konsolei</param>
        public void Swap2Lectures(int ID1, int ID2, ref string msg)
        {


            Lecture first = db.Lectures.Where(x => x.ID == ID1).Select(x => x).FirstOrDefault();
            Lecture second = db.Lectures.Find(ID2);
            if (isPossibleToChange(first, second))
            {

                msg = "Change  was successful.";
                int day = first.WeekdayID ?? 0;
                int time = first.LectureTimeID ?? 0;

                first.WeekdayID = second.WeekdayID;
                first.LectureTimeID = second.LectureTimeID;
                second.WeekdayID = day;
                second.LectureTimeID = time;

            }
            else
            {
                if (isPossibleToChangeGroup(first, second) && isPossibleToChangeGroup(second, first))
                {
                    Random rand = new Random();
                    bool isPossibleToChangeFirstTeacher = isPossibleToChangeTeacher(first, second);
                    bool isPossibleToChangeSecondTeacher = isPossibleToChangeTeacher(second, first);
                    bool isPossibleToChangeFirstClassRoom = isPossibleToChangeClassRoom(first, second);
                    bool isPossibleToChangeSecondClassRoom = isPossibleToChangeClassRoom(second, first);
                    Teacher firstTeacher = null;
                    Teacher secondTeacher = null;
                    ClassRoom firstClassRoom = null;
                    ClassRoom secondClassRoom = null;
                    if (isPossibleToChangeFirstTeacher == false)
                    {
                        Teacher[] teachers = db.Teachers.Where(x => x.Module == first.Teacher.Module && x.ID != first.TeacherID).Select(x => x).ToArray();
                        if (teachers.Length > 0)
                        {
                            Teacher[] valid = new Teacher[teachers.Length];
                            int count = 0;

                            foreach (Teacher teacher in teachers)
                            {
                                TimeSchedule teacherSchedule = Schedule(teacher.ID, "TeacherID");
                                if (teacherSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0) || second.TeacherID == teacher.ID)
                                {
                                    valid[count] = teacher;
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                firstTeacher = valid.ElementAt(rand.Next(0, count));
                                msg = "First lecture teacher was changed to " + firstTeacher.LastName + " " + firstTeacher.FirstName + ". ";
                            }
                        }
                    }
                    else
                        firstTeacher = first.Teacher;
                    if (isPossibleToChangeSecondTeacher == false)
                    {
                        Teacher[] teachers = db.Teachers.Where(x => x.Module == second.Teacher.Module && x.ID != second.TeacherID).Select(x => x).ToArray();
                        if (teachers.Length > 0)
                        {
                            Teacher[] valid = new Teacher[teachers.Length];
                            int count = 0;

                            foreach (Teacher teacher in teachers)
                            {
                                TimeSchedule teacherSchedule = Schedule(teacher.ID, "TeacherID");
                                if (teacherSchedule.isFree(first.WeekdayID ?? 0, first.LectureTimeID ?? 0) || first.TeacherID == teacher.ID)
                                {
                                    valid[count] = teacher;
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                secondTeacher = valid.ElementAt(rand.Next(0, count));
                                msg += "Second lecture teacher was changed to " + secondTeacher.LastName + " " + secondTeacher.FirstName + ". ";
                            }
                        }
                    }
                    else
                        secondTeacher = second.Teacher;
                    if (isPossibleToChangeFirstClassRoom == false)
                    {
                        ClassRoom[] classRooms;
                        if (first.IsPcRequired == true)
                        {
                            classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount && x.IsPCavailable == true).Select(x => x).ToArray();

                        }
                        else
                            classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount).Select(x => x).ToArray();
                        if (classRooms.Length > 0)
                        {
                            ClassRoom[] valid = new ClassRoom[classRooms.Length];
                            int count = 0;
                            foreach (ClassRoom classRoomm in classRooms)
                            {
                                TimeSchedule classRoomSchedule = Schedule(classRoomm.ID, "ClassRoomID");
                                if (classRoomSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0) || second.ClassRoomID == classRoomm.ID)
                                {
                                    valid[count] = classRoomm;
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                firstClassRoom = valid.ElementAt(rand.Next(0, count));
                                msg += "First lecture classroom was changed to " + firstClassRoom.Name + ". ";
                            }
                        }
                    }
                    else
                        firstClassRoom = first.ClassRoom;
                    if (isPossibleToChangeSecondClassRoom == false)
                    {
                        ClassRoom[] classRooms;
                        if (second.IsPcRequired == true)
                        {
                            classRooms = db.ClassRooms.Where(x => x.Type == second.ClassRoom.Type && x.ID != second.ClassRoomID && x.Type == second.Type && x.NumberOfPlaces >= second.Group.StudentsCount && x.IsPCavailable == true).Select(x => x).ToArray();
                        }
                        else
                            classRooms = db.ClassRooms.Where(x => x.Type == second.ClassRoom.Type && x.ID != second.ClassRoomID && x.Type == second.Type && x.NumberOfPlaces >= second.Group.StudentsCount).Select(x => x).ToArray();
                        if (classRooms.Length > 0)
                        {
                            ClassRoom[] valid = new ClassRoom[classRooms.Length];
                            int count = 0;
                            foreach (ClassRoom classRoomm in classRooms)
                            {
                                TimeSchedule classRoomSchedule = Schedule(classRoomm.ID, "ClassRoomID");
                                if (classRoomSchedule.isFree(first.WeekdayID ?? 0, first.LectureTimeID ?? 0) || first.ClassRoomID == classRoomm.ID)
                                {
                                    valid[count] = classRoomm;
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                secondClassRoom = valid.ElementAt(rand.Next(0, count));
                                msg += "Second lecture classroom was changed to " + firstClassRoom.Name + ". ";
                            }
                        }
                    }
                    else
                        secondClassRoom = second.ClassRoom;
                    if (firstTeacher != null && secondTeacher != null && firstClassRoom != null && secondClassRoom != null)
                    {
                        msg += "Change was successful.";
                        first.Teacher = firstTeacher;
                        first.TeacherID = firstTeacher.ID;
                        first.ClassRoom = firstClassRoom;
                        first.ClassRoomID = firstClassRoom.ID;
                        second.Teacher = secondTeacher;
                        second.TeacherID = secondTeacher.ID;
                        second.ClassRoom = secondClassRoom;
                        second.ClassRoomID = secondClassRoom.ID;
                        int day = first.WeekdayID ?? 0;
                        int time = first.LectureTimeID ?? 0;

                        first.WeekdayID = second.WeekdayID;
                        first.LectureTimeID = second.LectureTimeID;
                        second.WeekdayID = day;
                        second.LectureTimeID = time;
                    }
                    else
                        msg = "This change is impossible due to teachers/classrooms schedule.";
                }
                else msg = "This change is impossible due to groups schedule.";
            }
            db.SaveChanges();
        }
        /// <summary>
        /// Metodas, kuris pakeicia paskaitos laika, jei imanoma
        /// </summary>
        /// <param name="lectureId">ID paskaitos, kurios laika keisim </param>
        /// <param name="newLectureTimeId">Naujo laiko id</param>
        /// <param name="newLectureDayId">Naujos dienos id</param>
        /// <param name="msg">Pranesimas i konsole</param>
        public void ChangeLectureTime(int lectureId, int newLectureTimeId, int newLectureDayId, ref string msg)
        {
            Lecture first = db.Lectures.Find(lectureId);
            if (first.WeekdayID != null && first.LectureTimeID != null)
            {
                //Paskaitos dalyvių tvarkaraščiai
                TimeSchedule teacher = Schedule(first.TeacherID, "TeacherID");
                TimeSchedule classRoom = Schedule(first.ClassRoomID, "ClassRoomID");
                TimeSchedule group = Schedule(first.GroupID, "GroupID");

                //Patikrinam ar laisvi norimu laiku
                bool teacherIsFree = teacher.isFree(newLectureDayId, newLectureTimeId);
                bool classRoomrIsFree = classRoom.isFree(newLectureDayId, newLectureTimeId);
                bool groupIsFree = group.isFree(newLectureDayId, newLectureTimeId);
                //Jei visi laisvi, tai apkeičiam
                if (teacherIsFree && classRoomrIsFree && groupIsFree)
                {
                    first.WeekdayID = newLectureDayId;
                    first.LectureTimeID = newLectureTimeId;
                    msg = "Change was successful.";
                }
                else
                {
                    if (groupIsFree)
                    {
                        Lecture second = new Lecture();
                        second.LectureTimeID = newLectureTimeId;
                        second.WeekdayID = newLectureDayId;
                        bool isPossibleToChangeFirstTeacher = isPossibleToChangeTeacher(first, second);
                        bool isPossibleToChangeFirstClassRoom = isPossibleToChangeClassRoom(first, second);
                        Teacher firstTeacher = null;
                        ClassRoom firstClassRoom = null;
                        var rand = new Random();
                        if (isPossibleToChangeFirstTeacher == false)
                        {
                            Teacher[] teachers = db.Teachers.Where(x => x.Module == first.Teacher.Module && x.ID != first.TeacherID).Select(x => x).ToArray();
                            if (teachers.Length > 0)
                            {
                                Teacher[] valid = new Teacher[teachers.Length];
                                int count = 0;

                                foreach (Teacher teacherr in teachers)
                                {
                                    TimeSchedule teacherSchedule = Schedule(teacherr.ID, "TeacherID");
                                    if (teacherSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0))
                                    {
                                        valid[count] = teacherr;
                                        count++;
                                    }
                                }
                                if (count > 0)
                                {
                                    firstTeacher = valid.ElementAt(rand.Next(0, count));
                                    msg = "Lecture teacher was changed to " + firstTeacher.LastName + " " + firstTeacher.FirstName + ". ";
                                }
                            }
                        }
                        else
                            firstTeacher = first.Teacher;
                        if (isPossibleToChangeFirstClassRoom == false)
                        {
                            ClassRoom[] classRooms;
                            if (first.IsPcRequired == true)
                            {
                                classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount && x.IsPCavailable == true).Select(x => x).ToArray();
                            }
                            else
                                classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount).Select(x => x).ToArray();
                            if (classRooms.Length > 0)
                            {
                                ClassRoom[] valid = new ClassRoom[classRooms.Length];
                                int count = 0;
                                foreach (ClassRoom classRoomm in classRooms)
                                {
                                    TimeSchedule classRoomSchedule = Schedule(classRoomm.ID, "ClassRoomID");
                                    if (classRoomSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0))
                                    {
                                        valid[count] = classRoomm;
                                        count++;
                                    }
                                }
                                if (count > 0)
                                {
                                    firstClassRoom = valid.ElementAt(rand.Next(0, count));
                                    msg += "Lecture classroom was changed to " + firstClassRoom.Name + ". ";
                                }
                            }
                        }
                        else
                            firstClassRoom = first.ClassRoom;
                        if (firstTeacher != null && firstClassRoom != null)
                        {
                            first.Teacher = firstTeacher;
                            first.TeacherID = firstTeacher.ID;
                            first.ClassRoom = firstClassRoom;
                            first.ClassRoomID = firstClassRoom.ID;

                            msg += "Change was successful.";

                            first.WeekdayID = newLectureDayId;
                            first.LectureTimeID = newLectureTimeId;
                        }
                        else
                            msg = "This change is impossible due to teachers/classrooms schedule.";
                    }
                    else msg = "This change is impossible due to group schedule.";
                }
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Suranda visus galimus keitimo variantus
        /// </summary>
        /// <param name="ID">Lecture ID, kuriai reikai surasti keitimus</param>
        /// <returns>List galimu keitimu</returns>
        public List<Object> FindPossibleChanges(int ID, bool swapTeacher, bool swapClassroom)
        {
            //Reikia 
            List<Object> possibleChanges = new List<Object>();
                Lecture first = db.Lectures.Find(ID);
                List<Lecture> lectures = db.Lectures.ToList();
                foreach (Lecture lecture in lectures)
                {
                    if (lecture.ID != first.ID && (first.WeekdayID != lecture.WeekdayID || first.LectureTimeID != lecture.LectureTimeID))
                    {

                        if ((isPossibleToChangeGroup(first, lecture) && isPossibleToChangeGroup(lecture, first)) || first.GroupID == lecture.GroupID)
                        {
                            if (FullChange(first, lecture, swapTeacher, swapClassroom) && FullChange(lecture, first, swapTeacher, swapClassroom))
                            {                 
                                var x = new { ID = lecture.ID, SubjectN = lecture.Subject.Name, Day = lecture.Weekday.Name, Time = lecture.LectureTime.Start.ToString("g"), TeacherN = lecture.Teacher.LastName + " " + lecture.Teacher.FirstName  };
                                possibleChanges.Add(x);
                            }
                        }
                    }
                }
                List<Weekday> weekdays = db.Weekdays.ToList();
                List<LectureTime> lectureTimes = db.LectureTimes.ToList();
                foreach (Weekday day in weekdays)
                {
                    foreach (LectureTime time in lectureTimes)
                    {
                        Lecture artificial = new Lecture();
                        artificial.WeekdayID = day.ID;
                        artificial.Weekday = db.Weekdays.Find(day.ID);
                        artificial.LectureTimeID =  time.ID;
                        artificial.LectureTime = db.LectureTimes.Find(time.ID);
                        if (isPossibleToChangeGroup(first, artificial))
                        {

                            if (FullChange(first, artificial, swapTeacher, swapClassroom))
                            {
                                var x = new { ID = "", SubjectN = "", Day = artificial.Weekday.Name, DayId= artificial.WeekdayID, Time = artificial.LectureTime.Start.ToString("g"), TimeId = artificial.LectureTimeID, TeacherN =  " " };
                                possibleChanges.Add(x);
                            }
                        }
                    }
                }
                return possibleChanges;

        }
        /// <summary>
        /// Patikrina ar galima sukeisti dvi paskaitas pagal destytojus ir klases (su vartotojo nurodytais parametrais)
        /// </summary>
        /// <param name="first">Nurodyta paskaita</param>
        /// <param name="second">Viena is kitu paskaitu</param>
        /// <param name="swapTeacher">Ar galiam keisti destytoja</param>
        /// <param name="swapClassroom">Ar galima keisti auditorija</param>
        /// <returns>true or false</returns>
        private bool FullChange(Lecture first, Lecture second, bool swapTeacher, bool swapClassroom)
        {

            bool isTeacherFree = false;
            bool isClassRoomFree = false;
            if (isPossibleToChangeTeacher(first, second))
            {
                isTeacherFree = true;
            }
            else
            {
                if (swapTeacher)
                {
                    Teacher[] teachers = db.Teachers.Where(x => x.Module == first.Teacher.Module && x.ID != first.TeacherID).Select(x => x).ToArray();
                    if (teachers.Length > 0)
                    {
                        foreach (Teacher teacher in teachers)
                        {
                            TimeSchedule teacherSchedule = Schedule(teacher.ID, "TeacherID");
                            if (teacherSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0))
                            {
                                isTeacherFree = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (isPossibleToChangeClassRoom(first, second))
            {
                isClassRoomFree = true;
            }
            else
            {
                if (swapClassroom)
                {
                    ClassRoom[] classRooms;
                    if (first.IsPcRequired == true)
                    {
                        classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount && x.IsPCavailable == true).Select(x => x).ToArray();
                    }
                    else
                        classRooms = db.ClassRooms.Where(x => x.Type == first.ClassRoom.Type && x.ID != first.ClassRoomID && x.Type == first.Type && x.NumberOfPlaces >= first.Group.StudentsCount).Select(x => x).ToArray();
                    if (classRooms.Length > 0)
                    {
                        foreach (ClassRoom classRoom in classRooms)
                        {
                            TimeSchedule classRoomSchedule = Schedule(classRoom.ID, "ClassRoomID");
                            if (classRoomSchedule.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0))
                            {
                                isClassRoomFree = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (isClassRoomFree && isTeacherFree)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Patikrina ar galima apkeisti destytoja
        /// </summary>
        /// <param name="first">Paskaita, kurios destytojo keitima tikrins</param>
        /// <param name="second">Paskaita, kuriuos laiku turi buti laisvas destytojas</param>
        /// <returns>true or false</returns>
        private bool isPossibleToChangeTeacher(Lecture first, Lecture second)
        {
            if (first.WeekdayID != null && second.WeekdayID != null && first.LectureTimeID != null && second.LectureTimeID != null)
            {
                TimeSchedule firstTeacher = Schedule(first.TeacherID, "TeacherID");
                return firstTeacher.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
            }
            return false;
        }
        /// <summary>
        /// Patikrina ar galima apkeisti klase
        /// </summary>
        /// <param name="first">Paskaita, kurios klases keitima tikrins</param>
        /// <param name="second">Paskaita, kuriuos laiku turi buti laisva klase</param>
        /// <returns>true or false</returns>
        private bool isPossibleToChangeClassRoom(Lecture first, Lecture second)
        {
            if (first.WeekdayID != null && second.WeekdayID != null && first.LectureTimeID != null && second.LectureTimeID != null)
            {
                TimeSchedule firstClassRoom = Schedule(first.ClassRoomID, "ClassRoomID");
                return firstClassRoom.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
            }
            return false;
        }
        /// <summary>
        /// Patikrina ar galima apkeisti grupe
        /// </summary>
        /// <param name="first">Paskaita, kurios grupes keitima tikrins</param>
        /// <param name="second">Paskaita, kuriuos laiku turi buti laisva grupe</param>
        /// <returns>true or false</returns>
        private bool isPossibleToChangeGroup(Lecture first, Lecture second)
        {
            if (first.WeekdayID != null && second.WeekdayID != null && first.LectureTimeID != null && second.LectureTimeID != null)
            {
                TimeSchedule firstGroup = Schedule(first.GroupID, "ClassRoomID");
                return firstGroup.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
            }
            return false;
        }
        /// <summary>
        /// Grazina norima vieno objekto uzimtumo tvarkarasti
        /// </summary>
        /// <param name="id">Objekto ID</param>
        /// <param name="para">Objektas</param>
        /// <returns>Objekto uzimtumo tvarkarasti</returns>
        private TimeSchedule Schedule(int id, string para)
        {
            TimeSchedule schedule = new TimeSchedule(id, db.LectureTimes.Count());
            foreach (Lecture lecture in db.Lectures)
            {
                switch (para)
                {
                    case "TeacherID":
                        if (lecture.TeacherID == id && lecture.WeekdayID != null && lecture.LectureTimeID != null)
                        {
                            schedule.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                        }
                        break;
                    case "GroupID":
                        if (lecture.GroupID == id && lecture.WeekdayID != null && lecture.LectureTimeID != null)
                        {
                            schedule.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                        }
                        break;
                    case "ClassRoomID":
                        if (lecture.ClassRoomID == id && lecture.WeekdayID != null && lecture.LectureTimeID != null)
                        {
                            schedule.setTrue(lecture.WeekdayID ?? 0, lecture.LectureTimeID ?? 0);
                        }
                        break;
                    default:
                        break;
                }
            }
            return schedule;
        }
        /// <summary>
        /// Patikrina at keitimas yra galimas
        /// </summary>
        /// <param name="first">Pirma paskaita</param>
        /// <param name="second">Antra paskaita</param>
        /// <returns>true or false</returns>
        private bool isPossibleToChange(Lecture first, Lecture second)
        {
            if (first.WeekdayID != null && second.WeekdayID != null && first.LectureTimeID != null && second.LectureTimeID != null)
            {
                //Pirmos paskkaitos dalyvių tvarkaraščiai
                TimeSchedule firstTeacher = Schedule(first.TeacherID, "TeacherID");
                TimeSchedule firstClassRoom = Schedule(first.ClassRoomID, "ClassRoomID");
                TimeSchedule firstGroup = Schedule(first.GroupID, "GroupID");
                //Antros paskkaitos dalyvių tvarkaraščiai
                TimeSchedule secondTeacher = Schedule(second.TeacherID, "TeacherID");
                TimeSchedule secondClassRoom = Schedule(second.ClassRoomID, "ClassRoomID");
                TimeSchedule secondGroup = Schedule(second.GroupID, "GroupID");
                //Patikrinam ar laisvi
                bool firstTeacherIsFree = firstTeacher.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
                bool firstClassRoomrIsFree = firstClassRoom.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
                bool firstGroupIsFree = firstGroup.isFree(second.WeekdayID ?? 0, second.LectureTimeID ?? 0);
                bool secondTeacherIsFree = secondTeacher.isFree(first.WeekdayID ?? 0, first.LectureTimeID ?? 0);
                bool secondGroupIsFree = secondGroup.isFree(first.WeekdayID ?? 0, first.LectureTimeID ?? 0);
                bool secondClassRoomIsFree = secondClassRoom.isFree(first.WeekdayID ?? 0, first.LectureTimeID ?? 0);
                return (((firstTeacherIsFree && secondTeacherIsFree) || first.TeacherID == second.TeacherID) &&
                        ((firstClassRoomrIsFree && secondClassRoomIsFree) || first.ClassRoomID == second.ClassRoomID) &&
                        ((firstGroupIsFree && secondGroupIsFree) || first.GroupID == second.GroupID));
            }
            else return false;
        }

        /// <summary>
        /// Atsaukia keitima, jei yra imanoma
        /// </summary>
        /// <param name="logItemID"></param>
        public void Undo(int logItemID)
        {
            Log log = db.Logs.Find(logItemID);
            if (log.isUndoable)
            {
                if (log != null)
                {
                    Lecture first = db.Lectures.Find(log.FirstLectureID);
                    if (log.SecondLectureID != null)
                    {
                        Lecture second = db.Lectures.Find(log.SecondLectureID);


                        if (log.OldLectureFirstTeacherID == first.TeacherID && log.OldLectureFirstClassroomID == first.ClassRoomID && log.OldLectureSecondTeacherID == second.TeacherID && log.OldLectureSecondClassroomID == second.ClassRoomID)
                        {
                            if (isPossibleToChange(first, second))
                            {
                                int day = first.WeekdayID ?? 0;
                                int time = first.LectureTimeID ?? 0;

                                first.WeekdayID = second.WeekdayID;
                                first.LectureTimeID = second.LectureTimeID;
                                second.WeekdayID = day;
                                second.LectureTimeID = time;
                                db.Logs.Remove(log);
                                db.SaveChanges();
                            }
                            else
                            {
                                UndoMessage(first, second, log);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            int fTID = first.TeacherID;
                            int fCID = first.ClassRoomID;
                            int sTID = second.TeacherID;
                            int sCID = second.ClassRoomID;
                            first.TeacherID = log.OldLectureFirstTeacherID;
                            first.Teacher = db.Teachers.Find(first.TeacherID);
                            first.ClassRoomID = log.OldLectureFirstClassroomID;
                            first.ClassRoom = db.ClassRooms.Find(first.ClassRoomID);
                            second.TeacherID = log.OldLectureSecondTeacherID ?? 0;
                            second.Teacher = db.Teachers.Find(second.TeacherID);
                            second.ClassRoomID = log.OldLectureSecondClassroomID ?? 0;
                            second.ClassRoom = db.ClassRooms.Find(second.ClassRoomID);
                            if (isPossibleToChange(first, second))
                            {
                                int day = first.WeekdayID ?? 0;
                                int time = first.LectureTimeID ?? 0;

                                first.WeekdayID = second.WeekdayID;
                                first.LectureTimeID = second.LectureTimeID;
                                second.WeekdayID = day;
                                second.LectureTimeID = time;
                                db.Logs.Remove(log);
                                db.SaveChanges();
                            }
                            else
                            {
                                UndoMessage(first, second, log);
                                first.TeacherID = fTID;
                                first.Teacher = db.Teachers.Find(first.TeacherID);
                                first.ClassRoomID = fCID;
                                first.ClassRoomID = log.OldLectureFirstClassroomID;
                                second.TeacherID = sTID;
                                second.Teacher = db.Teachers.Find(second.TeacherID);
                                second.ClassRoomID = sCID;
                                second.ClassRoom = db.ClassRooms.Find(second.ClassRoomID);
                                db.SaveChanges();
                            }
                        }


                    }
                    else
                    {
                        if (log.OldLectureFirstTeacherID == first.TeacherID && log.OldLectureFirstClassroomID == first.ClassRoomID)
                        {
                            //Paskaitos dalyvių tvarkaraščiai
                            TimeSchedule teacher = Schedule(first.TeacherID, "TeacherID");
                            TimeSchedule classRoom = Schedule(first.ClassRoomID, "ClassRoomID");
                            TimeSchedule group = Schedule(first.GroupID, "GroupID");

                            //Patikrinam ar laisvi norimu laiku
                            bool teacherIsFree = teacher.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            bool classRoomrIsFree = classRoom.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            bool groupIsFree = group.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            //Jei visi laisvi, tai apkeičiam
                            if (teacherIsFree && classRoomrIsFree && groupIsFree)
                            {
                                first.WeekdayID = log.OldWeekdayID ?? 0;
                                first.LectureTimeID = log.OldLectureTimeID ?? 0;
                                db.Logs.Remove(log);
                                db.SaveChanges();
                            }
                            else
                            {

                                UndoMessage(first, log);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            int fTID = first.TeacherID;
                            int fCID = first.ClassRoomID;
                            first.TeacherID = log.OldLectureFirstTeacherID;
                            first.ClassRoomID = log.OldLectureFirstClassroomID;
                            first.Teacher = db.Teachers.Find(first.TeacherID);
                            first.ClassRoom = db.ClassRooms.Find(first.ClassRoomID);
                            //Paskaitos dalyvių tvarkaraščiai
                            TimeSchedule teacher = Schedule(first.TeacherID, "TeacherID");
                            TimeSchedule classRoom = Schedule(first.ClassRoomID, "ClassRoomID");
                            TimeSchedule group = Schedule(first.GroupID, "GroupID");

                            //Patikrinam ar laisvi norimu laiku
                            bool teacherIsFree = teacher.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            bool classRoomrIsFree = classRoom.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            bool groupIsFree = group.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
                            //Jei visi laisvi, tai apkeičiam
                            if (teacherIsFree && classRoomrIsFree && groupIsFree)
                            {
                                first.WeekdayID = log.OldWeekdayID ?? 0;
                                first.LectureTimeID = log.OldLectureTimeID ?? 0;
                                db.Logs.Remove(log);
                                db.SaveChanges();
                            }
                            else
                            {
                                UndoMessage(first, log);
                                first.TeacherID = fTID;
                                first.ClassRoomID = fCID;
                                first.Teacher = db.Teachers.Find(first.TeacherID);
                                first.ClassRoom = db.ClassRooms.Find(first.ClassRoomID);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Pranesimas konsolei, jei neimanomas undo
        /// </summary>
        /// <param name="first">Pirma paskaita</param>
        /// <param name="second">Antra paskaita</param>
        /// <param name="log">Keitimo logas</param>
        private void UndoMessage(Lecture first, Lecture second, Log log)
        {
            int index1 = log.LogItem.IndexOf("Message");
            if (index1 != -1)
            {
                string result = log.LogItem.Remove(index1);
                log.LogItem = result;
            }
            if (!isPossibleToChangeTeacher(first, second))
            {

                string msg = " Message: Undo is impossible due to " + first.Teacher.FirstName + " " + first.Teacher.LastName + " schedule";
                log.LogItem += msg;
                return;
            }

            if (!isPossibleToChangeTeacher(second, first))
            {
                string msg = "Undo is impossible due to " + second.Teacher.FirstName + " " + second.Teacher.LastName + " schedule";
                log.LogItem += msg;
                return;

            }
            if (!isPossibleToChangeClassRoom(first, second))
            {
                string msg = " Undo is impossible due to " + first.ClassRoom.Name + " schedule";
                log.LogItem += msg;
                return;
            }

            if (!isPossibleToChangeClassRoom(second, first))
            {
                string msg = " Undo is impossible due to " + second.ClassRoom.Name + " schedule";
                log.LogItem += msg;
                return;
            }
            if (!isPossibleToChangeGroup(first, second))
            {
                string msg = " Undo is impossible due to " + first.Group.Name + " schedule";
                log.LogItem += msg;
                return;
            }

            if (!isPossibleToChangeGroup(second, first))
            {
                string msg = " Undo is impossible due to " + second.Group.Name + " schedule";
                log.LogItem += msg;
            }
        }

        /// <summary>
        /// Pranesimas konsolei, jei neimanoams undo
        /// </summary>
        /// <param name="first">Paskaita</param>
        /// <param name="log">logas</param>
        private void UndoMessage(Lecture first, Log log)
        {
            int index1 = log.LogItem.IndexOf("Message");
            if (index1 != -1)
            {
                string result = log.LogItem.Remove(index1);
                log.LogItem = result;
            }
            TimeSchedule teacher = Schedule(first.TeacherID, "TeacherID");
            bool teacherIsFree = teacher.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
            if (!teacherIsFree)
            {
                string msg = " Undo is impossible due to " + first.Teacher.FirstName + " " + first.Teacher.LastName + " schedule";
                log.LogItem += msg;
                return;
            }
            TimeSchedule classRoom = Schedule(first.ClassRoomID, "ClassRoomID");
            bool classRoomIsFree = classRoom.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
            if (!classRoomIsFree)
            {
                string msg = " Undo is impossible due to " + first.Teacher.FirstName + " " + first.Teacher.LastName + " schedule";
                log.LogItem += msg;
                return;
            }
            TimeSchedule group = Schedule(first.GroupID, "GroupID");
            bool groupIsFree = group.isFree(log.OldWeekdayID ?? 0, log.OldLectureTimeID ?? 0);
            if (!groupIsFree)
            {
                string msg = " Undo is impossible due to " + first.Teacher.FirstName + " " + first.Teacher.LastName + " schedule";
                log.LogItem += msg;
                return;
            }
        }
    }
}