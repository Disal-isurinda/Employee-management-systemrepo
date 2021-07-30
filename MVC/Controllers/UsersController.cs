using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class UsersController : Controller
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
            return View();
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
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
    }
}