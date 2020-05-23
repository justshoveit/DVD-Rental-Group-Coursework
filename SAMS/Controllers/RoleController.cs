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
using SAMS.Core;

namespace SAMS.Controllers
{

    [AuthorizeUser(Roles = "Admin, Teacher")]
    public class RoleController : Controller
    {
        private RoleLogic _logic;

        public RoleController()
        {
            _logic = new RoleLogic();
        }


        // GET: /Role/
        public async Task<ActionResult> Index()
        {
            List<Role> lst = await _logic.GetRoles();
            return View(lst);
        }

        // GET: /Role/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Role obj = await _logic.GetRole(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /Role/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Role role)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateRole(role);
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
                return View(role);
            }
        }

        // GET: /Role/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Role obj = await _logic.GetRole(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Role role)
        {
            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateRole(role);
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
                return View(role);
            }
        }

        // GET: /Role/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Role obj = await _logic.GetRole(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteRole(id);
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
