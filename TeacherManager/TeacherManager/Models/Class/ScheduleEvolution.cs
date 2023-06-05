using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherManager.Models.Class
{
    public class ScheduleEvolution
    {
        private TeacherWorkEntities db = new TeacherWorkEntities();
        List<TIME_SLOT> tIME_SLOTs = new List<TIME_SLOT>();
        private readonly Random _random;
        ~ScheduleEvolution()
        {

        }
        public List<TIME_SLOT> GetTIME_SLOTs()
        {
            return tIME_SLOTs;
        }
        public ScheduleEvolution()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public SchedulePopulation GetOptimalSchedulePopulation(SchedulePopulation population, int generationsCount, CLASSROOM classrooms)
        {
            var currentPopulation = population;

            for (int i = 0; i < generationsCount; i++)
            {
                //sx lịch tăng dần theo độ tương thích
                var orderedPopulation = currentPopulation.Schedules.OrderBy(p => p.GetFitness());
                //lấy 10 tương thích cao nhất
                var bestSchedules = orderedPopulation.Take(10).ToList();

                //tạo quần thể mới 
                var newPopulation = new SchedulePopulation(currentPopulation.Schedules.Count, bestSchedules.First().Teacher, bestSchedules.First().Day);
                foreach (var schedule in newPopulation.Schedules)
                {
                    //schedule.Teacher.Schedules.Add(schedule);
                    var courses = schedule.GetCoursesOnDay();

                    foreach (var course in courses)
                    {
                        //var slots = course.Days.Find(d => d.Name == schedule.Day.DayOfWeek.ToString()).Slots;

                        var slots = courses
                           .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
                           .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
                           .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
                           .Where(temp => temp.day.NAME.ToString() == schedule.Day.DayOfWeek.ToString() && temp.subject.ID == course.ID)
                           .Select(temp => temp.timeSlot);

                        // check if time slot is taken by another class or teacher
                        List<TIME_SLOT> availableSlots = new List<TIME_SLOT>();
                        foreach (var item in slots)
                        {
                            if (!HasClassOrTeacherTakenSlot(classrooms, schedule.Day, item))
                            {
                                availableSlots.Add(item);
                            }
                        }
                        //var availableSlots = slots.Where(s => !HasClassOrTeacherTakenSlot(classrooms, schedule.Day, s)).ToList();

                        if (availableSlots.Count == 0)
                        {
                            continue;
                        }

                        var randomSlotIndex = _random.Next(availableSlots.Count);
                        if (tIME_SLOTs.All(m => m.ID != availableSlots[randomSlotIndex].ID))
                            this.tIME_SLOTs.Add(availableSlots[randomSlotIndex]);

                    }
                }

                currentPopulation = newPopulation;
            }

            return currentPopulation;
        }
        private bool HasClassOrTeacherTakenSlot(CLASSROOM Lclassrooms, DateTime date, TIME_SLOT slot)
        {
            //foreach (var item in Lclassrooms)
            //{
            var courses = db.CLASSROOMs
                .Join(db.SUBJECTs, classroom => classroom.ID, subject => subject.ID_CLASSROOM, (classroom, subject) => new { classroom, subject })
                .Join(db.ARRANGE_TIME_SLOT, cs => cs.subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (cs, arrangeTimeSlot) => new { cs.classroom, cs.subject, arrangeTimeSlot })
                .Join(db.TIME_SLOT, ca => ca.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (ca, timeSlot) => new { ca.classroom, ca.subject, ca.arrangeTimeSlot, timeSlot })
                .Join(db.DAYs, cdat => cdat.timeSlot.ID_DAY, day => day.ID, (cdat, day) => new
                {
                    cdat.subject,
                    cdat.arrangeTimeSlot,
                    cdat.timeSlot,
                    day
                }).Where(m => m.subject.CLASSROOM.ID == Lclassrooms.ID && m.day.NAME.ToString() == date.DayOfWeek.ToString() && date > m.subject.START_DAY && date < m.subject.END_DAY)
                .Select(m => m.subject)
                .ToList();

            //var courses = classroom.Courses.Where(c => c.Days.Exists(d => d.Name == day.DayOfWeek.ToString())).ToList();

            foreach (var course in courses)
            {
                var slots = db.SUBJECTs
               .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
               .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
               .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
               .Where(temp => temp.day.NAME.ToString() == date.DayOfWeek.ToString() && temp.arrangeTimeSlot.ID_SUBJECT == course.ID)
               .Select(temp => temp.timeSlot)
               .ToList();
                //DS slot của môn trong 1 ngày của 1 lớp
                //var slots = db.SUBJECTs
                //   .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
                //   .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
                //   .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
                //   .Where(temp => temp.day.NAME.ToString() == date.DayOfWeek.ToString() && temp.subject.ID == course.ID && temp.subject.ID==temp.arrangeTimeSlot.ID_SUBJECT)
                //   .Select(temp => temp.timeSlot)
                //   .ToList();
                //var slots = course.Days.Find(d => d.Name == day.DayOfWeek.ToString()).Slots;

                if (slots.Any(s => s.ID == slot.ID))
                {

                    //trùng
                    return false;
                }
            }
            //}
            //ko trùng

            return true;
        }


    }
}
