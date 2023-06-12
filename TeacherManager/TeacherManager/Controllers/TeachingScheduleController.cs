using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherManager.Models;
using TeacherManager.Models.Class;

namespace TeacherManager.Controllers
{
    public class TeachingScheduleController : Controller
    {
        TeacherWorkEntities db = new TeacherWorkEntities();
        // GET: TeachingSchedule
        public ActionResult Index()
        {
            TEACHER tEACHER = db.TEACHERs.Where(m => m.ID == 1).First();
            DateTime date = new DateTime(2023, 06, 05);
            var ls = GetWeek(date);
            ViewBag.ls = ls;
            ViewBag.Mon = tEACHER.GetSUBJECTs(ls[1]);
            ViewBag.Tue = tEACHER.GetSUBJECTs(ls[2]);
            ViewBag.Wed = tEACHER.GetSUBJECTs(ls[3]);
            ViewBag.Thur = tEACHER.GetSUBJECTs(ls[4]);
            ViewBag.Fri = tEACHER.GetSUBJECTs(ls[5]);
            ViewBag.Sat = tEACHER.GetSUBJECTs(ls[6]);
            return View();
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
            var result = db.CLASSROOMs
            .Join(db.SUBJECTs, classroom => classroom.ID, subject => subject.ID_CLASSROOM, (classroom, subject) => new { classroom, subject })
            .Where(temp => temp.subject.ID_TEACHER == 1)
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
            TEACHER tEACHER = db.TEACHERs.Where(m => m.ID == 1).First();
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
            Schedule schedule1 = new Schedule();
            Schedule a = new Schedule();
            foreach (var item in list)
            {
                if (item.SlotAvailible(timeslotsls).Count >= numberLesson)
                {
                  a= item;
                    break;
                }

            }
            ViewBag.schedule = a;
            return View("Show");
        }

        public ActionResult Show()
        {
           
            return View();
        }

        public JsonResult LoadSubjects(int classroomId)
        {
            var subjects = db.SUBJECTs
                .Where(s => s.ID_CLASSROOM == classroomId && s.TEACHER.ID == 1)
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
    }
}