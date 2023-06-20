using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherManager.Models;
using TeacherManager.Models.Class;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace TeacherManager.Controllers
{
    public class TeachingScheduleController : Controller
    {
      
        TeacherWorkEntities db = new TeacherWorkEntities();
        [Authorize]
        
        // GET: TeachingSchedule
        public ActionResult Timetable()
        {
            return View();
        }

        public ActionResult GetEventsTimetable(DateTime startOfWeek)
        {
            string ID_USER = User.Identity.GetUserId();
            TEACHER teacher = db.TEACHERs.FirstOrDefault(t => t.ID_USER==ID_USER);

            List<Event> scheduleList = new List<Event>();

            DateTime endOfWeek = startOfWeek.AddDays(6);
            for (var date = startOfWeek.Date; date <= endOfWeek.Date; date = date.AddDays(1))
            {
                List<SUBJECT> subjectList = teacher.GetSUBJECTs(date);
                List<MAKEUP_LESSON> mAKEUP_LESSONs = db.MAKEUP_LESSON.Where(m => m.DATE == date && m.SUBJECT.TEACHER.ID==teacher.ID).ToList();
                foreach(var item in mAKEUP_LESSONs)
                {
                    Event schedule = new Event();
                    schedule.title = "LỊCH BÙ\nMôn: " + item.SUBJECT.NAME + "\nLớp: " + item.CLASSROOM.NAME;
                    schedule.start = date.ToString("yyyy-MM-dd") + 'T' + item.TIMESTART;
                    schedule.end = date.ToString("yyyy-MM-dd") + 'T' + item.TIMEEND;
                    schedule.className = "event-" + (item.ID % 5);
                    scheduleList.Add(schedule);
                }
                foreach (var subject in subjectList)
                {
                    List<TIME_SLOT> timeSlotList = subject.GetTIME_SLOTs(date);

                    Event schedule = new Event();
                    schedule.title = "Môn: " + subject.NAME + "\nLớp: " + subject.CLASSROOM.NAME;
                    schedule.start = date.ToString("yyyy-MM-dd") + 'T' + timeSlotList.FirstOrDefault().NAME;
                    schedule.end = date.ToString("yyyy-MM-dd") + 'T' + timeSlotList.LastOrDefault().NAME;
                    schedule.className = "event-" + (subject.ID % 5);
                    scheduleList.Add(schedule);
                }
            }

            if (HttpContext.Cache["scheduleList"] != null)
            {
                HttpContext.Cache.Remove("scheduleList"); // Xóa danh sách sự kiện cũ khỏi cache
            }

            HttpContext.Cache.Insert("scheduleList", scheduleList, null, DateTime.Now.AddDays(1), TimeSpan.Zero); // Lưu danh sách sự kiện mới vào cache

            return Json(scheduleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamTimetable()
        {
            return View();
        }

        public ActionResult GetEventsExamsTimetable(DateTime startOfWeek)
        {
            string ID_USER = User.Identity.GetUserId();
            TEACHER teacher = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);

            List<Event> scheduleList = new List<Event>();

            DateTime endOfWeek = startOfWeek.AddDays(6);
            for (var date = startOfWeek.Date; date <= endOfWeek.Date; date = date.AddDays(1))
            {
                // Thêm điều kiện kiểm tra tEST_SCHEDULEs.Count > 0 để đảm bảo chỉ lấy sự kiện lịch thi khi có ít nhất một kỳ thi được sắp xếp
                List<TEST_SCHEDULE> tEST_SCHEDULEs = teacher.GetExamsSUBJECTs(date);
                if (tEST_SCHEDULEs.Count > 0)
                {
                    foreach (var item in tEST_SCHEDULEs)
                    {
                        Event schedule = new Event();
                        schedule.title = "Môn: " + item.SUBJECT.NAME + " - " + item.ROOM.NAME_ROM;
                        schedule.start = date.ToString("yyyy-MM-dd") + 'T' + item.TIMESTART;
                        TimeSpan timestart = TimeSpan.ParseExact(item.TIMESTART, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
                        TimeSpan time = TimeSpan.ParseExact(item.TIME, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
                        TimeSpan timeend = timestart.Add(time);

                        schedule.end = date.ToString("yyyy-MM-dd") + 'T' + timeend.ToString();
                        schedule.className = "event-" + (item.ID % 5);
                        scheduleList.Add(schedule);
                    }
                }
            }

            if (HttpContext.Cache["scheduleList"] != null)
            {
                HttpContext.Cache.Remove("scheduleList"); // Xóa danh sách sự kiện cũ khỏi cache
            }

            HttpContext.Cache.Insert("scheduleList", scheduleList, null, DateTime.Now.AddDays(1), TimeSpan.Zero); // Lưu danh sách sự kiện mới vào cache

            return Json(scheduleList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RegistertoSchedule()
        {
            TEACHER tEACHER = db.TEACHERs.Where(m => m.ID == 1).First();
            var cLASSROOM = db.CLASSROOMs.ToList();
            DateTime date = new DateTime(2023, 06, 05);
            Schedule schedule = new Schedule(tEACHER, date);

            // Khởi tạo lịch dạy bù cho giảng viên
            var startDate = DateTime.Today.AddDays(7); // bắt đầu tính từ tuần sau
            var schedulePopulation = new SchedulePopulation(7, tEACHER, startDate);

            //Tối ưu hóa với thuật toán di truyền
            var evolution = new ScheduleEvolution();
            var optimalSchedules = evolution.GetOptimalSchedulePopulation(schedulePopulation, 10, cLASSROOM[0]);

            var timeslotsls = evolution.GetTIME_SLOTs().ToList();
            ViewBag.timeslotsls = timeslotsls;
            int time = 5;
            var list = optimalSchedules.Schedules.OrderBy(m => m.GetFitness());
            Schedule schedule1 = new Schedule();
            foreach (var item in list)
            {
                if (item.SlotAvailible(timeslotsls).Count >= time)
                {
                    ViewBag.schedule = item;
                    break;
                }

            }
            return View();
        }

        public ActionResult Register()
        {

            string ID_USER = User.Identity.GetUserId();
            TEACHER teacher = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);

            var result = db.CLASSROOMs
            .Join(db.SUBJECTs, classroom => classroom.ID, subject => subject.ID_CLASSROOM, (classroom, subject) => new { classroom, subject })
            .Where(temp => temp.subject.ID_TEACHER == teacher.ID)
            .GroupBy(temp => temp.classroom.ID)
            .Select(group => group.FirstOrDefault().classroom)
            .ToList();
            ViewBag.Classrooms = new SelectList(result, "ID", "NAME");
            ViewBag.Subject = new SelectList(db.SUBJECTs.Where(m => m.ID_TEACHER==1), "ID", "NAME");
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {

            string ID_USER = User.Identity.GetUserId();
            TEACHER tEACHER = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);
            // Lấy các giá trị từ FormCollection (form) bằng phương thức TryGetValue,
            // và thực hiện các xử lý tương ứng,
            // ví dụ: lấy giá trị của dropdownlist "Classrooms"
            int Idclassrooms;
            int.TryParse(form["Classrooms"], out Idclassrooms);

            // giá trị của dropdownlist "Subjects"
            int Idsubjects;
            int.TryParse(form["Subjects"], out Idsubjects);

            // giá trị của trường nhập "number_lesson"
            int numberLesson;
            int.TryParse(form["number_lesson"], out numberLesson);

            // xử lý các giá trị này theo yêu cầu
            var cLASSROOM = db.CLASSROOMs.Where( m=> m.ID==Idclassrooms).First();
            Schedule schedule = new Schedule(tEACHER, DateTime.Now);

            // Khởi tạo lịch dạy bù cho giảng viên
            var startDate = DateTime.Today.AddDays(7); // bắt đầu tính từ tuần sau
            var schedulePopulation = new SchedulePopulation(7, tEACHER, startDate);

            //Tối ưu hóa với thuật toán di truyền
            var evolution = new ScheduleEvolution();
            var optimalSchedules = evolution.GetOptimalSchedulePopulation(schedulePopulation, 10, cLASSROOM);

            var timeslotsls = evolution.GetTIME_SLOTs().ToList();
            ViewBag.timeslotsls = timeslotsls;
            var list = optimalSchedules.Schedules.OrderBy(m => m.GetFitness());
            Schedule temp = new Schedule();
            foreach (var item in list)
            {
                if (item.SlotAvailible(timeslotsls).Count >= numberLesson)
                {
                  temp = item;
                    break;
                }

            }
            List<TIME_SLOT> lsTimeSlot = temp.SlotAvailible(ViewBag.timeslotsls);
            ViewBag.timeStart = lsTimeSlot.OrderBy(s => s.ID).FirstOrDefault().NAME;
            var timeTemp = lsTimeSlot.ElementAt(numberLesson-1).NAME;
            TimeSpan time = TimeSpan.Parse("00:50:00");
            TimeSpan timeend = TimeSpan.ParseExact(timeTemp, "hh\\:mm\\:ss", CultureInfo.InvariantCulture).Add(time);
            ViewBag.timeEnd = timeend.ToString();
            ViewBag.Schedule = temp;
            ViewBag.ClassName = db.CLASSROOMs.Find(Idsubjects);
            ViewBag.SubjectName = db.SUBJECTs.Find(Idclassrooms);
            return View("Show");
        }



        public ActionResult Show()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult AddMakeupLesson(string classname,string  subjectname, DateTime day, string timestart, string timeend)
        {
            int IdClass = db.CLASSROOMs.Where(m => m.NAME == classname).First().ID;
            int IdSubject = db.SUBJECTs.Where(m => m.NAME == subjectname).First().ID;
            MAKEUP_LESSON mAKEUP_LESSON = new MAKEUP_LESSON();
            mAKEUP_LESSON.ID_CLASS = IdClass;
            mAKEUP_LESSON.ID_SUBJECT = IdSubject;
            mAKEUP_LESSON.DATE = day;
            mAKEUP_LESSON.TIMESTART = timestart;
            mAKEUP_LESSON.TIMEEND = timeend;
            mAKEUP_LESSON.SITUATION = "Đang chờ duyệt";
            db.MAKEUP_LESSON.Add(mAKEUP_LESSON);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }



        public JsonResult LoadSubjects(int classroomId)
        {
            string ID_USER = User.Identity.GetUserId();
            TEACHER tEACHER = db.TEACHERs.FirstOrDefault(t => t.ID_USER == ID_USER);

            var subjects = db.SUBJECTs
                .Where(s => s.ID_CLASSROOM == classroomId && s.TEACHER.ID == tEACHER.ID )
                .Select(s => new { Value = s.ID, Text = s.NAME })
                .ToList();
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }

        // Nhập vào một ngày có trong tuần và trả về danh sách các ngày trong tuần đó
        public List<DateTime> GetWeek(DateTime date)
        {
            // Tìm ngày đầu tiên trong tuần chứa ngày đó
            DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);

            // Tạo danh sách các ngày trong tuần chứa ngày đó
            List<DateTime> weekDays = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                weekDays.Add(startOfWeek.AddDays(i));
            }
            return weekDays;
        }

        public bool HasConsecutiveNumbers(List<int> numbers)
        {
            int count = 1;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] == numbers[i + 1] - 1)
                {
                    count++;
                    if (count == 5)
                    {
                        // Tìm thấy 5 số liên tiếp
                        return true;
                    }
                }
                else
                {
                    count = 1;
                }
            }

            // Không tìm thấy 5 số liên tiếp
            return false;
        }

    }
}