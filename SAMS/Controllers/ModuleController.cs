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
using SAMS.Core;

namespace SAMS.Controllers
{
    [AuthorizeUser(Roles = "Admin,Teacher")]
    public class ModuleController : Controller
    {
        private ModuleLogic _logic;
        private CourseLogic _logicCourse;
        AttendanceRepository _attendanceRepo;

        public ModuleController()
        {
            _logic = new ModuleLogic();
            _logicCourse = new CourseLogic();
            _attendanceRepo = new AttendanceRepository();
        }

        // GET: /Timetable/
        public async Task<ActionResult> Index()
        {
            List<Module> lst = await _logic.GetModules();
            return View(lst);
        }

        // GET: /Timetable/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Module obj = await _logic.GetModule(id);
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
            List<Semester> lstSemester = await _attendanceRepo.GetSemesters();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name");
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name");
            return View();
        }

        // POST: /Timetable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Module module)
        {

            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Semester> lstSemester = await _attendanceRepo.GetSemesters();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name");
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name");
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateModule(module);
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
                return View(module);
            }

        }

        // GET: /Timetable/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Module obj = await _logic.GetModule(id);
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Semester> lstSemester = await _attendanceRepo.GetSemesters();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name", obj.CourseId);
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name", obj.SemesterId);
            return View(obj);
        }

        // POST: /Timetable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Module module)
        {

            Module obj = await _logic.GetModule(module.Id);
            List<Course> lstCourses = await _logicCourse.GetCourses();
            List<Semester> lstSemester = await _attendanceRepo.GetSemesters();
            ViewBag.CourseId = new SelectList(lstCourses, "Id", "Name", obj.CourseId);
            ViewBag.SemesterId = new SelectList(lstSemester, "Id", "Name", obj.SemesterId);
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateModule(module);
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
                return View(module);
            }

        }

        // GET: /Timetable/Delete/5
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            Module obj = await _logic.GetModule(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteModule(id);
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
