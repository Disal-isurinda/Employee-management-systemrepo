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
    public class UsersController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,LoginDate,LoginTime")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,LoginDate,LoginTime")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
            usersModel.Roles = RolesList;
            return View(usersModel);

        }
        [HttpPost]
        public ActionResult Register(aspnet_Users usersModel)
        {
            MembershipCreateStatus memCreSta;
            Membership.CreateUser(usersModel.username, usersModel.password, usersModel.email, usersModel.passwordQuestion, usersModel.passwordAnswer, usersModel.isApproved, usersModel empID, out memCreSta);
            if (memCreSta == MembershipCreateStatus.Success)
            {
                System.Web.Security.Roles.AddUsersToRole(new[] { usersModel.username }, usersModel.Roles[0]);
                return RedirectToAction("Index", "Home");
            }
            return View(usersModel);
        }
        [HttpPost]
        public ActionResult Logon(aspnet_Users usersModel, string returnUrl)
        {


            // var currentUser =  db.Users.Where(w => w.UserName == User.UserName && w.Password == User.Password).FirstOrDefault();

            var currentUser = Membership.ValidateUser(usersModel.username, usersModel.password);
            if (currentUser)
            {
                HttpContext.Session["UserName"] = usersModel.username;

                FormsAuthentication.SetAuthCookie(usersModel.username, true);

                var urlSplit = returnUrl.Split('\\');
                return RedirectToAction(urlSplit[1], urlSplit[0], new { });
                //return RedirectToAction("Index","Home", new { });
            }
            HttpContext.Session["UserName"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult RoleCreation(aspnet_Roles rm)
        {
            System.Web.Security.Roles.CreateRole(rm.RoleName);
            return View();

        }

    }

}
