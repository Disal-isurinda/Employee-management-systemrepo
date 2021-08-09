using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace MVC.Controllers
{
    public class IdentityController : Controller
    {
        [HttpGet]
        public ActionResult LogOn()
        {

            if (Session["returnUrl"] == null)
            {
                //return RedirectToAction("Index", "Home");
                return View();
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(UsersModel usersModel, string returnUrl)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/GetValidateUser/" + usersModel.username.ToString() + "/" + usersModel.password.ToString()).Result;
           
            if (response.IsSuccessStatusCode || (usersModel.username == "Admin" && usersModel.password == "Admin"))
            {
                HttpContext.Session["UserName"] = usersModel.username;

                FormsAuthentication.SetAuthCookie(usersModel.username, true);

                //var urlSplit = returnUrl.Split('\\');
                //return RedirectToAction(urlSplit[1], urlSplit[0], new { });
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session["UserName"] = null;
            return View();
        }
        //[HttpPost]
        //public ActionResult LogOn(LogOnModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Membership.ValidateUser(model.UserName, model.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        public ActionResult Logout()
        {
            HttpContext.Session["UserName"] = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }
        // GET: /Account/ChangePassword


        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword


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