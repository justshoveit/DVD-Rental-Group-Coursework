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
using SAMS.Core;

namespace SAMS.Controllers
{
    [AuthorizeUser(Roles="Admin, Teacher")]
    public class UserController : Controller
    {
        private UserLogic _logic;
        private RoleLogic _logicRole;

        public UserController()
        {
            _logic = new UserLogic();
            _logicRole = new RoleLogic();
        }

        // GET: /User/
        public async Task<ActionResult> Index()
        {
            List<User> lst = await _logic.GetUsers();
            return View(lst); ;
        }

        // GET: /User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            User obj = await _logic.GetUser(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: /User/Create
        public async Task<ActionResult> Create()
        {
            List<Role> lstRole = await  _logicRole.GetRoles();
            ViewBag.RoleList = lstRole.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            //ViewBag.RoleId = new SelectList(lstRole, "Id", "Name");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserName,Password,Email,RoleId")] User user)
        {
            List<Role> lstRole = await _logicRole.GetRoles();
            ViewBag.RoleList = lstRole.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            if (user.Password != user.ReTypePassword)
            {
                ViewBag.Message = "Password and Re-type Password does not match";
                return View(user);
            }

            bool status = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateUser(user);
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
                return View(user);
            }
        }

        // GET: /User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            List<Role> lstRole = await _logicRole.GetRoles();
            User user = await _logic.GetUser(id);
            ViewBag.RoleId = new SelectList(lstRole, "Id", "Name",user.Id);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Password,Email")] User user)
        {
            bool status = false;
            string message = string.Empty;

            if (user.Password != user.ReTypePassword)
            {
                ViewBag.Message = "Password and Re-type Password does not match";
                return View(user);
            }

            List<Role> lstRole = await _logicRole.GetRoles();
            User userInfo = await _logic.GetUser(user.Id);
            ViewBag.RoleId = new SelectList(lstRole, "Id", "Name", userInfo.Id);
            if (ModelState.IsValid)
            {
                try
                {
                    status = await _logic.InsertUpdateUser(user);
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
                return View(user);
            }
        }

        // GET: /User/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            User obj = await _logic.GetUser(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool status = await _logic.DeleteUser(id);
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
