using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAMS;
using SAMS.Logic;
using SAMS.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SAMS.Repository.AttendanceRepository;

namespace SAMS.Controllers
{
    public class TimetableController : Controller
    {
        private TimetableLogic _logic;
        private CourseLogic _logicCourse;
        private ModuleLogic _logicModule;
        private TeacherLogic _logicTeacher;
        private AttendanceRepository _repo = null;

        public TimetableController()
        {
            _logic = new TimetableLogic();
            _logicCourse = new CourseLogic();
            _logicModule = new ModuleLogic();
            _logicTeacher = new TeacherLogic();
            _repo = new AttendanceRepository();
        }

        // GET: /Timetable/
        public async Task<ActionResult> Index()
        {
            List<Timetable> lst = await _logic.GetTimetables();
            return View(lst);
        }

        // GET: /Timetable/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Timetable obj = await _logic.GetTimetable(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /Timetable/Create
        public async Task<ActionResult> Create()
        {
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Module> lstModules = await _logicModule.GetModules();
            List<Teacher> lstTeachers = await _logicTeacher.GetTeachers();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name");
            ViewBag.ModuleId = new SelectList(lstModules, "Id", "Name");
            ViewBag.TeacherId = new SelectList(lstTeachers, "Id", "Name");

            //
            List<Semester> lstSemester = await _repo.GetSemesters();
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name");
            return View();
        }

        // POST: /Timetable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseId,SemesterId,AcademicYear,ModuleId,TeacherId,StartTime,EndTime,Day")] Timetable timetable)
        {
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Module> lstModules = await _logicModule.GetModules();
            List<Teacher> lstTeachers = await _logicTeacher.GetTeachers();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name");
            ViewBag.ModuleId = new SelectList(lstModules, "Id", "Name");
            ViewBag.TeacherId = new SelectList(lstTeachers, "Id", "Name");
            List<Semester> lstSemester = await _repo.GetSemesters();
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name");
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateTimeTable(timetable);
                }
                catch (SqlException ex)
                {

                    message = ex.Message;
                }
                catch (Exception)
                {

                    message = "Unexpected error was found";
                }

            }
            if (status)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = message;
                return View(timetable);
            }

        }

        // GET: /Timetable/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Timetable obj = await _logic.GetTimetable(id);
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Module> lstModules = await _logicModule.GetModules();
            List<Teacher> lstTeachers = await _logicTeacher.GetTeachers();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name", obj.CourseId);
            ViewBag.ModuleId = new SelectList(lstModules, "Id", "Name", obj.ModuleId);
            ViewBag.TeacherId = new SelectList(lstTeachers, "Id", "Name", obj.TeacherId);

            List<Semester> lstSemester = await _repo.GetSemesters();
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name", obj.SemesterId);
            return View(obj);
        }

        // POST: /Timetable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CourseId,SemesterId,AcademicYear,ModuleId,TeacherId,StartTime,EndTime,Day")] Timetable timetable)
        {
            Timetable obj = await _logic.GetTimetable(timetable.Id);
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Module> lstModules = await _logicModule.GetModules();
            List<Teacher> lstTeachers = await _logicTeacher.GetTeachers();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name", obj.CourseId);
            ViewBag.ModuleId = new SelectList(lstModules, "Id", "Name", obj.ModuleId);
            ViewBag.TeacherId = new SelectList(lstTeachers, "Id", "Name", obj.TeacherId);

            List<Semester> lstSemester = await _repo.GetSemesters();
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name", obj.SemesterId);
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateTimeTable(timetable);
                }
                catch (SqlException ex)
                {

                    message = ex.Message;
                }
                catch (Exception)
                {

                    message = "Unexpected error was found";
                }

            }
            if (status)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = message;
                return View(timetable);
            }

        }

        // GET: /Timetable/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           Timetable obj = await _logic.GetTimetable(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                  bool status = await _logic.DeleteTimetable(id);
            return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    if (ex.Errors[0].Number == 547)
                    {
                        ViewBag.Message = "This data can not be deleted,because it is in use.";
                        return View();
                    }

                }
                ViewBag.Message = "Database error occurred";
                return View();

            }
            catch (Exception)
            {
                ViewBag.Message = "Something went wrong while deleting the record.";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // _logic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
