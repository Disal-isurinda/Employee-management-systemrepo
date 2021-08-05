using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static MVC.Models.mvcUserModel;

namespace MVC.Controllers
{
    public class IdentityController : Controller
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
        [HttpPost]
        public ActionResult Register(UsersModel usersModel)
        {
            MembershipCreateStatus memCreSta;
            Membership.CreateUser(usersModel.username, usersModel.password, usersModel.email, usersModel.passwordQuestion, usersModel.passwordAnswer, usersModel.isApproved, out memCreSta);
            if (memCreSta == MembershipCreateStatus.Success)
            {
                System.Web.Security.Roles.AddUsersToRole(new[] { usersModel.username }, usersModel.Roles[0]);
                return RedirectToAction("Index", "Home");
            }
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

        //[HttpPost]
        //public ActionResult Logon(UsersModel usersModel, string returnUrl)
        //{


        //    // var currentUser =  db.Users.Where(w => w.UserName == User.UserName && w.Password == User.Password).FirstOrDefault();

        //    var currentUser = Membership.ValidateUser(usersModel.username, usersModel.password);
        //    if (currentUser)
        //    {
        //        HttpContext.Session["UserName"] = usersModel.username;

        //        FormsAuthentication.SetAuthCookie(usersModel.username, true);

        //        var urlSplit = returnUrl.Split('\\');
        //        return RedirectToAction(urlSplit[1], urlSplit[0], new { });
        //        //return RedirectToAction("Index","Home", new { });
        //    }
        //    HttpContext.Session["UserName"] = null;
        //    return View();
        //}
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult Logout()
        {
            HttpContext.Session["UserName"] = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion


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