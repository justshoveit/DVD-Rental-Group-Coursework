using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using SAMS.Logic;
using SAMS.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SAMS.Core;

namespace SAMS.Controllers
{
    [AuthorizeUser(Roles = "Admin,Teacher")]
    public class StudentController : Controller
    {
        private StudentLogic _logic;

        public StudentController()
        {
            _logic = new StudentLogic();
        }

        // GET: /Student/
        public async Task<ActionResult> Index()
        {
            List<Student> lst = await _logic.GetStudents();
            return View(lst);
        }

        // GET: /Student/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Student obj = await _logic.GetStudent(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,RollNumber,Email,AcademicYear,EnrollDate")] SAMS.Models.Student student)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateStudent(student);
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
                return View(student);
            }



        }

        // GET: /Student/Edit/5Inde
        public async Task<ActionResult> Edit(int id)
        {

            // Student student = db.Students.Find(id);
            SAMS.Models.Student student = await _logic.GetStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RollNumber,Email,AcademicYear,EnrollDate")] SAMS.Models.Student student)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateStudent(student);
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
                return View(student);
            }

        }

        // GET: /Student/Delete/5
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
          
             Student obj = await _logic.GetStudent(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteStudent(id);
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
            catch(Exception)
            {
                ViewBag.Message = "Something went wrong while deleting the record.";
                return View();
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
