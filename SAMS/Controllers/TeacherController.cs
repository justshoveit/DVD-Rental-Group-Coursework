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

namespace SAMS.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherLogic _logic;

        public TeacherController()
        {
            _logic = new TeacherLogic();
        }


        // GET: /Teacher/
        public async Task<ActionResult> Index()
        {
            List<SAMS.Models.Teacher> teachers = await _logic.GetTeachers();
            return View(teachers);
        }

        // GET: /Teacher/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Teacher teacher = await _logic.GetTeacher(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {

             ViewBag.Types = new List<SelectListItem>(){
            new SelectListItem(){Value="1",Text="Tutor"},
            new SelectListItem(){Value="2",Text="Lecture"}
        };
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Type,Email,MobileNo")] Teacher teacher)
        {
            ViewBag.Types = new List<SelectListItem>(){
            new SelectListItem(){Value="1",Text="Tutor"},
            new SelectListItem(){Value="2",Text="Lecture"}
        };
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateTeacher(teacher);
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
                return View(teacher);
            }

        }

        // GET: /Teacher/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Types = new List<SelectListItem>(){
          new SelectListItem(){Value="1",Text="Tutor"},
          new SelectListItem(){Value="2",Text="Lecture"}
          };
            SAMS.Models.Teacher teacher = await _logic.GetTeacher(id);
            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Type,Email,MobileNo")] Teacher teacher)
        {
            ViewBag.Types = new List<SelectListItem>(){
          new SelectListItem(){Value="1",Text="Tutor"},
          new SelectListItem(){Value="2",Text="Lecture"}
          };
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateTeacher(teacher);
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
                return View(teacher);
            }

        }

        // GET: /Teacher/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            SAMS.Models.Teacher teacher = await _logic.GetTeacher(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool a = await _logic.DeleteTeacher(id);
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
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
