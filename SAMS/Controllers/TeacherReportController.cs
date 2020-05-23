using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAMS.Models;
using SAMS.ViewModels;
using SAMS.Repository.AttendanceRepository;
using SAMS.Repository.ReportRepository;
using SAMS.Core;

namespace SAMS.Controllers
{
    [AuthorizeUser(Roles = "Admin,Teacher")]
    public class TeacherReportController : Controller
    {
        private AttendanceRepository _repo;
        private ReportRepository _reportRepo;
        private ApplicationDbContext db = new ApplicationDbContext();
        public TeacherReportController()
        {
            _repo = new AttendanceRepository();
            _reportRepo = new ReportRepository();
        }
        // GET: Attendance
        public async Task<ActionResult> Index()
        {
            List<SelectListItem> TeacherDLL = new List<SelectListItem>(){
                new SelectListItem { Text = "All Teachers", Value = "-1" },
                new SelectListItem { Text = "Tutors", Value = "1" },
                new SelectListItem { Text = "Lecturers", Value = "2" },
            }.ToList();
            ViewBag.TeacherTypeDDL = TeacherDLL;
            return View();
        }


        // GET: Attendance/Details/5
        public async Task<ActionResult> Details(TeacherReport data)
        {
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<TeacherReport> report = await _reportRepo.AdministrativeReport(data.TeacherTypeId);

            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Attendance/Create
        public async Task<ActionResult> Create()
        {
            AttendanceViewModel model = new AttendanceViewModel();

            if (HttpContext.IsDebuggingEnabled)
            {
                model.CourseList = new List<SelectListItem>(){
                new SelectListItem(){Text = "Business Administration(BBA)", Value = "1"},
                new SelectListItem(){Text = "Information Technology(BIT)", Value = "2"},
            };
                model.SemesterList = new List<SelectListItem>(){
                new SelectListItem(){ Text = "Semester 1", Value = "1" },
                new SelectListItem(){ Text = "Semester 2", Value = "2" },
                new SelectListItem(){ Text = "Semester 3", Value = "3" },
                new SelectListItem(){ Text = "Semester 4", Value = "4" },
                new SelectListItem(){ Text = "Semester 5", Value = "5" },
                new SelectListItem(){ Text = "Semester 6", Value = "6" },
                new SelectListItem(){ Text = "Semester 7", Value = "7" },
                new SelectListItem(){ Text = "Semester 8", Value = "8" }
            };
            }
            else
            {
                var courses = await _repo.GetCourses();
                model.CourseList = courses.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                var semesters = await _repo.GetSemesters();
                model.SemesterList = semesters.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }

            model.ClassList = new List<SelectListItem>();

            return View(model);
        }

        // POST: Attendance/Create
        [HttpPost]
        public async Task<ActionResult> Create(AttendanceMaster attendanceMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!HttpContext.IsDebuggingEnabled)
                    {
                        await _repo.SaveAttendance(attendanceMaster);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(attendanceMaster);
        }

               [HttpGet]
        public async Task<ActionResult> GetClasses(int courseId, int semesterId)
        {
            SAMS.Models.AttendanceMaster param = new SAMS.Models.AttendanceMaster()
            {
                CourseId = courseId,
                SemesterId = semesterId,
                Day = (int)DateTime.Now.DayOfWeek
            };
            //List<SAMS.Models.Timetable> list = await _repo.GetTimetable(param);
            List<SAMS.Models.Timetable> list = new List<Models.Timetable>
            {
                new Models.Timetable{Id=1, ClassDetails= "Data Algorithms | 10:00-11:00 | Sunday"},
                new Models.Timetable{Id=1, ClassDetails= "Descrete Logics | 11:00-12:00 | Sunday"}
            };
            var ddl = list.Select(a => new
            {
                Text = a.ClassDetails,
                Value = a.Id
            }).ToList();
            return Json(ddl, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PopulateDetails(int courseId, int semesterId, int timetableId)
        {
            AttendanceMaster param = new AttendanceMaster
            {
                CourseId = courseId,
                SemesterId = semesterId,
                TimetableId = timetableId
            };
            //List<SAMS.Models.Student> students = await _repo.GetStudents(param);
            List<SAMS.Models.Student> students = new List<Models.Student>{
                new Models.Student{Id=1, Name = "Sam Smith"},
                new Models.Student{Id=2, Name = "Anil Einstine"},
            };

            List<AttendanceDetailViewModel> details = students.Select(x => new AttendanceDetailViewModel
            {
                StudentId = x.Id,
                StudentName = x.Name
            }).ToList();

            return PartialView(details);
        }

        [HttpGet]
        public async Task<JsonResult> GetCourseList()
        {
            try
            {
                List<SAMS.Models.Course> list;
                list = await _repo.GetCourses();
                
                var ddl = list.Select(a => new
                {
                    Text = a.Name,
                    Value = a.Id
                }).ToList();
                return Json(ddl, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetTimetableList(int courseId, int semesterId)
        {
            try
            {
                SAMS.Models.AttendanceMaster param = new SAMS.Models.AttendanceMaster()
                    {
                        CourseId = courseId,
                        SemesterId = semesterId,
                        Day = (int)DateTime.Now.DayOfWeek
                    };
                List<SAMS.Models.Timetable> list;
                if (HttpContext.IsDebuggingEnabled)
                {
                    list = new List<Models.Timetable>()
                {
                    new Models.Timetable{Id=1, ClassDetails= "Data Algorithms | 10:00-11:00 | Sunday"},
                    new Models.Timetable{Id=1, ClassDetails= "Descrete Logics | 11:00-12:00 | Sunday"}
                };
                }
                else
                    list = await _repo.GetTimetable(param);

                var ddl = list.Select(a => new
                {
                    Text = a.ClassDetails,
                    Value = a.Id
                }).ToList();
                return Json(ddl, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetStudentList(int courseId, int semesterId, int timetableId)
        {
            try
            {
                AttendanceMaster param = new AttendanceMaster
                {
                    CourseId = courseId,
                    SemesterId = semesterId,
                    TimetableId = timetableId
                };
                List<SAMS.Models.Student> students;
                if (HttpContext.IsDebuggingEnabled)
                {
                    students = new List<Models.Student>{
                    new Models.Student{Id=1, Name = "Sam Smith"},
                    new Models.Student{Id=2, Name = "Anil Einstine"},
                };
                }
                else
                    students = await _repo.GetStudents(param);
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveAttendance(AttendanceMaster data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!HttpContext.IsDebuggingEnabled)
                    {
                        await _repo.SaveAttendance(data);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
