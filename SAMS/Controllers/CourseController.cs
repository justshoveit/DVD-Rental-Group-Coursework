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
using System.Data.SqlClient;

namespace SAMS.Controllers
{
    public class CourseController : Controller
    {
        private CourseLogic _logic;

        public CourseController()
        {
            _logic = new CourseLogic();

        }

        // GET: /Course/
        public async Task<ActionResult> Index()
        {
            List<Course> lst = await _logic.GetCourses();
            return View(lst);
        }
     
        // GET: /Course/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Course obj = await _logic.GetCourse(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] Course course)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateCourse(course);
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
                return View(course);
            }
        }

        // GET: /Course/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await _logic.GetCourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] Course course)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateCourse(course);
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
                return View(course);
            }
        }

        // GET: /Course/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           Course obj = await _logic.GetCourse(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteCourse(id);
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
