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
using System.Threading.Tasks;
using SAMS.Models;
using SAMS.Core;
using System.Data.SqlClient;

namespace SAMS.Controllers
{
    [AuthorizeUser(Roles = "Admin,Teacher")]
    public class StudentModuleMappingController : Controller
    {
        private StudentModuleLogic _logic;
        private ModuleLogic _logicModule;
        private StudentLogic _logicStudent;

        public StudentModuleMappingController()
        {
            _logic = new StudentModuleLogic();
            _logicModule = new ModuleLogic();
            _logicStudent = new StudentLogic();
        }

        // GET: /StudentModuleMapping/
        public async Task<ActionResult> Index()
        {
            List<StudentModuleMapping> lst = await _logic.GetStudentModuleMappings();
            return View(lst);
        }

        // GET: /StudentModuleMapping/Details/5
        public async Task<ActionResult> Details(int id)
        {
            StudentModuleMapping obj = await _logic.GetStudentModuleMapping(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /StudentModuleMapping/Create
        public async Task<ActionResult> Create()
        {
            List<Module> lstModule = await _logicModule.GetModules();
            List<Student> lstStudents = await _logicStudent.GetStudents();
            ViewBag.ModuleId = new SelectList(lstModule, "Id", "Name");
            ViewBag.StudentId = new SelectList(lstStudents, "Id", "Name");
            return View();
        }

        // POST: /StudentModuleMapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StudentId,ModuleId")] StudentModuleMapping studentmodulemapping)
        {
            List<Module> lstModule = await _logicModule.GetModules();
            List<Student> lstStudents = await _logicStudent.GetStudents();
            ViewBag.ModuleId = new SelectList(lstModule, "Id", "Name");
            ViewBag.StudentId = new SelectList(lstStudents, "Id", "Name");
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateStudentModuleMapping(studentmodulemapping);
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
                return View(studentmodulemapping);
            }
        }

        // GET: /StudentModuleMapping/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            StudentModuleMapping  obj = await _logic.GetStudentModuleMapping(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            List<Module> lstModule = await _logicModule.GetModules();
            List<Student> lstStudents = await _logicStudent.GetStudents();
            ViewBag.ModuleId = new SelectList(lstModule, "Id", "Name", obj.ModuleId);
            ViewBag.StudentId = new SelectList(lstStudents, "Id", "Name", obj.StudentId);
            return View(obj);
        }

        // POST: /StudentModuleMapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentId,ModuleId")] StudentModuleMapping studentmodulemapping)
        {
            List<Module> lstModule = await _logicModule.GetModules();
            List<Student> lstStudents = await _logicStudent.GetStudents();
            ViewBag.ModuleId = new SelectList(lstModule, "Id", "Name", studentmodulemapping.ModuleId);
            ViewBag.StudentId = new SelectList(lstStudents, "Id", "Name", studentmodulemapping.StudentId);
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateStudentModuleMapping(studentmodulemapping);
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
                return View(studentmodulemapping);
            }
        }

        // GET: /StudentModuleMapping/Delete/5
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            StudentModuleMapping obj = await _logic.GetStudentModuleMapping(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /StudentModuleMapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteStudentModuleMapping(id);
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
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
