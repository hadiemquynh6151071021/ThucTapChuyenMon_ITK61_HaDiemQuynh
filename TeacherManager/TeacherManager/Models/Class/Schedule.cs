using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManager.Models.Class
{
    public class Schedule
    {
        private TeacherWorkEntities db = new TeacherWorkEntities();
        public TEACHER Teacher { get; set; }

        public DateTime Day { get; set; }
        ~Schedule()
        {
        }
        public Schedule(TEACHER teacher, DateTime day)
        {
            Teacher = teacher;
            Day = day;
        }
        public Schedule()
        {
        }

        public List<SUBJECT> GetCoursesOnDay()
        {

            //DS MÔN GV DẠY TRONG 1 NGÀY CỤ THỂ
            var result = db.SUBJECTs
                .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
                .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
                .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
                .Where(temp => temp.subject.ID_TEACHER == Teacher.ID && temp.day.NAME.ToString() == Day.DayOfWeek.ToString() && Day > temp.subject.START_DAY && Day < temp.subject.END_DAY)
                .GroupBy(temp => temp.subject.ID)
                .Select(group => group.FirstOrDefault().subject)
                .ToList();


            return result;

        }
        //public List<TIME_SLOT> Full_Slots()
        //{
        //    var courses = GetCoursesOnDay();
        //    List<TIME_SLOT> slot_full = new List<TIME_SLOT>();

        //    foreach (var course in courses)
        //    {
        //        var slots = db.SUBJECTs
        //        .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
        //        .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
        //        .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
        //        .Where(temp => temp.subject.ID_TEACHER == Teacher.ID && temp.day.NAME.ToString() == Day.DayOfWeek.ToString() && temp.subject.ID == course.ID)
        //        .Select(temp => temp.timeSlot)
        //        .ToList();
        //        slot_full.AddRange(slots);
        //    }

        //    return slot_full;
        //}

        //TÌM SLOT GV CÒN TRỐNG TRONG NGÀY
        //public List<TIME_SLOT> Empty_Slots()
        //{


        //    var slots_all_day = db.TIME_SLOT.Where(m => m.DAY.NAME.ToString() == Day.DayOfWeek.ToString());
        //    return slots_all_day.ToList().Except(Full_Slots().ToList(), new Time_Slot_Comparer()).ToList();
        //}
        //public List<SUBJECT> GetSUBJECTs(CLASSROOM cLASSROOM, DateTime date)
        //{
        //    var courses = db.CLASSROOMs
        //            .Join(db.SUBJECTs, classroom => classroom.ID, subject => subject.ID_CLASSROOM, (classroom, subject) => new { classroom, subject })
        //            .Join(db.ARRANGE_TIME_SLOT, cs => cs.subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (cs, arrangeTimeSlot) => new { cs.classroom, cs.subject, arrangeTimeSlot })
        //            .Join(db.TIME_SLOT, ca => ca.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (ca, timeSlot) => new { ca.classroom, ca.subject, ca.arrangeTimeSlot, timeSlot })
        //            .Join(db.DAYs, cdat => cdat.timeSlot.ID_DAY, day => day.ID, (cdat, day) => new
        //            {
        //                cdat.subject,
        //                cdat.arrangeTimeSlot,
        //                cdat.timeSlot,
        //                day
        //            }).Where(m => m.subject.CLASSROOM.ID == cLASSROOM.ID && m.day.NAME.ToString() == date.DayOfWeek.ToString())
        //            .Select(m => m.subject)
        //            .ToList();
        //    return courses;
        //}
        //public int CountDayOnListCourse()
        //{

        //    // Tính tổng số ngày học của giáo viên dạy các môn được phân công trong 1 tuần
        //    int totalDays = db.SUBJECTs
        //                    .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
        //                    .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
        //                    .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
        //                    .Where(temp => temp.subject.ID_TEACHER == Teacher.ID)
        //                    .GroupBy(temp => temp.subject.ID)
        //                    .Select(group => group.FirstOrDefault().subject)
        //                    .ToList()
        //                    .Count();
        //    return totalDays;
        //}
        public double GetFitness()
        {
            var courses = GetCoursesOnDay();
            //số khóa học của gv
            var a = db.SUBJECTs.Where(s => s.ID_TEACHER == Teacher.ID).Count();

            //nếu ds trống thì độ tương thích là 0
            if (courses.Count == 0)
            {
                return 1;
            }
            else
            {

                var fitness = 0.0;

                foreach (var course in courses)
                {
                    var slots = db.SUBJECTs
                    .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
                    .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
                    .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
                    .Where(temp => temp.subject.ID_TEACHER == Teacher.ID && temp.day.NAME.ToString() == Day.DayOfWeek.ToString() && temp.subject.ID == course.ID)
                    .Select(temp => temp.timeSlot)
                    .ToList();

                    //calculate fitness based on assigned time slots
                    //var assignedSlotsCount = slots.Count;
                    //fitness += (double)assignedSlotsCount / (double)Empty_Slots().Count;
                    //fitness += (double)slots.Count / (double)(a * CountDayOnListCourse())*100;
                    fitness += (double)slots.Count;
                }

                //return Math.Round(1-(fitness / courses.Count)/100, 2);
                return Math.Round(fitness / 10, 2);
            }
        }


        public List<TIME_SLOT> SlotAvailible(List<TIME_SLOT> tIME_SLOTs, DateTime date)
        {
            var timeSlotsOnDay = db.TIME_SLOT.Where(s => s.DAY.NAME == date.DayOfWeek.ToString());
            var timeSlotsFull = tIME_SLOTs.Where(s => s.DAY.NAME == date.DayOfWeek.ToString());

            return timeSlotsOnDay.ToList().Except(timeSlotsFull.ToList(), new Time_Slot_Comparer()).ToList();
        }


    }
    class Time_Slot_Comparer : IEqualityComparer<TIME_SLOT>
    {
        public bool Equals(TIME_SLOT x, TIME_SLOT y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(TIME_SLOT obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}