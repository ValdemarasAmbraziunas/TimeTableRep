using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;

namespace TimeTable.Services
{
    public interface ISwapService
    {
        void Swap2Lectures(int ID1, int ID2, ref string msg);
        void ChangeLectureTime(int lectureId, int newLectureTimeId, int newLectureDayId, ref string msg);
        List<Object> FindPossibleChanges(int ID, bool swapTeacher, bool swapClassroom);
        void Undo(int logItemID);

    }
}