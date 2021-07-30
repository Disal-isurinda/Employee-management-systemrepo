using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class aspnet_UsersController : Controller
    {
        private DBModel db = new DBModel();

        // GET: aspnet_Users
        public ActionResult Index()
        {
            var aspnet_Users = db.aspnet_Users.Include(a => a.aspnet_Applications).Include(a => a.aspnet_Membership).Include(a => a.aspnet_Profile).Include(a => a.Employee);
            return View(aspnet_Users.ToList());
        }

        // GET: aspnet_Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            if (aspnet_Users == null)
            {
                return HttpNotFound();
            }
            return View(aspnet_Users);
        }

        // GET: aspnet_Users/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName");
            ViewBag.UserId = new SelectList(db.aspnet_Membership, "UserId", "Password");
            ViewBag.UserId = new SelectList(db.aspnet_Profile, "UserId", "PropertyNames");
            ViewBag.EmpID = new SelectList(db.Employees, "EmpID", "FirstName");
            return View();
        }

        // POST: aspnet_Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,UserId,UserName,LoweredUserName,MobileAlias,IsAnonymous,LastActivityDate,EmpID")] aspnet_Users aspnet_Users)
        {
            if (ModelState.IsValid)
            {
                aspnet_Users.UserId = Guid.NewGuid();
                db.aspnet_Users.Add(aspnet_Users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Users.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Membership, "UserId", "Password", aspnet_Users.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Profile, "UserId", "PropertyNames", aspnet_Users.UserId);
            ViewBag.EmpID = new SelectList(db.Employees, "EmpID", "FirstName", aspnet_Users.EmpID);
            return View(aspnet_Users);
        }

        // GET: aspnet_Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            if (aspnet_Users == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Users.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Membership, "UserId", "Password", aspnet_Users.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Profile, "UserId", "PropertyNames", aspnet_Users.UserId);
            ViewBag.EmpID = new SelectList(db.Employees, "EmpID", "FirstName", aspnet_Users.EmpID);
            return View(aspnet_Users);
        }

        // POST: aspnet_Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,UserId,UserName,LoweredUserName,MobileAlias,IsAnonymous,LastActivityDate,EmpID")] aspnet_Users aspnet_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnet_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_Users.ApplicationId);
            ViewBag.UserId = new SelectList(db.aspnet_Membership, "UserId", "Password", aspnet_Users.UserId);
            ViewBag.UserId = new SelectList(db.aspnet_Profile, "UserId", "PropertyNames", aspnet_Users.UserId);
            ViewBag.EmpID = new SelectList(db.Employees, "EmpID", "FirstName", aspnet_Users.EmpID);
            return View(aspnet_Users);
        }

        // GET: aspnet_Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            if (aspnet_Users == null)
            {
                return HttpNotFound();
            }
            return View(aspnet_Users);
        }

        // POST: aspnet_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            db.aspnet_Users.Remove(aspnet_Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Register()
        {

            string[] Roles = System.Web.Security.Roles.GetAllRoles();
            List<string> RolesList = new List<string>();
            foreach (var r in Roles)
            {
                if (HttpContext.User.IsInRole("Admin"))
                {
                    RolesList.Add(r);
                }
                else
                {
                    if (r != "Admin")
                    {
                        RolesList.Add(r);
                    }
                }


            }
            aspnet_Users usersModel = new aspnet_Users();
            usersModel.aspnet_Roles = (ICollection<aspnet_Roles>)RolesList;
            return View(usersModel);

        }
        [HttpPost]
        public ActionResult Register(aspnet_Users usersModel)
        {
            MembershipCreateStatus memCreSta;
            Membership.CreateUser(usersModel.UserName, usersModel.Password, usersModel.email, usersModel.passwordQuestion, usersModel.passwordAnswer, usersModel.isApproved, out memCreSta);
            if (memCreSta == MembershipCreateStatus.Success)
            {
                System.Web.Security.Roles.AddUsersToRole(new[] { usersModel.UserName }, usersModel.Roles[0]);

                return RedirectToAction("Index", "Home");
            }
            return View(usersModel);
        }
    }
}
