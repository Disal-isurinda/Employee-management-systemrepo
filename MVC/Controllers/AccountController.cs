using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static MVC.Models.MembershipProvider;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
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
            UsersModel usersModel = new UsersModel();
            usersModel.Roles = RolesList;
            return View(usersModel);

        }
        [HttpGet]
        public ActionResult Logon()
        {

            if (Session["returnUrl"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logon(UsersModel usersModel, string returnUrl)
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

        public ActionResult Logout()
        {
            HttpContext.Session["UserName"] = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult RoleCreation()
        {
            if (HttpContext.User.IsInRole("Admin"))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpPost]
        public ActionResult RoleCreation(RolesModel rm)
        {
            System.Web.Security.Roles.CreateRole(rm.RoleName);
            return View();

        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}