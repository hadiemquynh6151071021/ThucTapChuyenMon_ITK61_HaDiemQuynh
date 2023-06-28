using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherManager.Models;
using TeacherManager.Models.Class;

namespace TeacherManager.Controllers
{
    public class HomeController : Controller
    {
        TeacherWorkEntities db = new TeacherWorkEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetApplicaMakeupLessonForTeacher()
        {
            return View();
        }

        //public ActionResult GetEvents()
        //{
        //    string ID_USER = User.Identity.GetUserId();
        //    TEACHER tEACHER = db.TEACHERs.Where(m => m.ID_USER.Equals(ID_USER)).First();

        //    var result = db.SUBJECTs
        //      .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
        //      .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
        //      .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
        //      .Where(temp => temp.subject.ID_TEACHER == tEACHER.ID  && DateTime.Now > temp.subject.START_DAY && DateTime.Now < temp.subject.END_DAY)
        //      .GroupBy(temp => temp.subject.ID)
        //      .Select(group => group.FirstOrDefault().subject)
        //      .ToList();
        //    List<Event> events = new List<Event>();

        //    // Lấy danh sách các môn học của giảng viên
        //   // List<SUBJECT> subjects = db.TEACHERs.Find(1).SUBJECTs.ToList();

        //    // Duyệt qua các môn học và lấy thông tin lịch học
        //    foreach (SUBJECT subject in result)
        //    {
        //        List<TIME_SLOT> timeSlots = subject.GetTIME_SLOTs(DateTime.Now);

        //        foreach (TIME_SLOT timeSlot in timeSlots)
        //        {
        //            Event newEvent = new Event();
        //            newEvent.id = subject.ID.ToString() + "_" + timeSlot.ID.ToString();
        //            newEvent.title = subject.NAME + " - " + subject.CLASSROOM.NAME;
        //            newEvent.start = DateTime.Now;
        //            newEvent.end = DateTime.Now.AddDays(2);
        //            events.Add(newEvent);
        //        }
        //    }

        //    return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            //string ID_USER = User.Identity.GetUserId();
            //TEACHER teacher = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);

            //List<Event> scheduleList = new List<Event>();
            //DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            //DateTime endOfWeek = startOfWeek.AddDays(6);
            //for (var date = startOfWeek.Date; date <= endOfWeek.Date; date = date.AddDays(1))
            //{
            //    List<SUBJECT> subjectList = teacher.GetSUBJECTs(date);
            //    foreach (SUBJECT subject in subjectList)
            //    {
            //        List<TIME_SLOT> timeSlotList = subject.GetTIME_SLOTs(date);
 
            //            Event schedule = new Event();
            //            schedule.title = subject.NAME;
            //            schedule.start = date.ToString("yyyy-MM-dd") + 'T' +timeSlotList.FirstOrDefault().NAME;
            //            schedule.end = date.ToString("yyyy-MM-dd") + 'T' + timeSlotList.LastOrDefault().NAME;
            //            schedule.className = "event-" + (subject.ID % 5);

            //            scheduleList.Add(schedule);
            //    }
            //}

            //ViewBag.ScheduleList = JsonConvert.SerializeObject(scheduleList);

            return View();
        }

        //public ActionResult GetEvents(DateTime startOfWeek)
        //{
        //    string ID_USER = User.Identity.GetUserId();
        //    TEACHER teacher = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);

        //    List<Event> scheduleList = new List<Event>();

        //    DateTime endOfWeek = startOfWeek.AddDays(6);
        //    for (var date = startOfWeek.Date; date <= endOfWeek.Date; date = date.AddDays(1))
        //    {
        //        List<SUBJECT> subjectList = teacher.GetSUBJECTs(date);
        //        foreach (SUBJECT subject in subjectList)
        //        {
        //            List<TIME_SLOT> timeSlotList = subject.GetTIME_SLOTs(date);

        //            Event schedule = new Event();
        //            schedule.title = subject.NAME;
        //            schedule.start = date.ToString("yyyy-MM-dd") + 'T' + timeSlotList.FirstOrDefault().NAME;
        //            schedule.end = date.ToString("yyyy-MM-dd") + 'T' + timeSlotList.LastOrDefault().NAME;
        //            schedule.className = "event-" + (subject.ID % 5);

        //            scheduleList.Add(schedule);
        //        }
        //    }

        //    return Json(scheduleList, JsonRequestBehavior.AllowGet);
        //}


    }
}