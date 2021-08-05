using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using CompareAttribute = System.Web.Mvc.CompareAttribute;

namespace MVC.Models
{
        public class UsersModel
        {
            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            //public UsersModel()
            //{
            //    this.User = new HashSet<UsersModel>();
            //}
            public string username { get; set; }

            public string password { get; set; }

            public string email { get; set; }

            public string passwordQuestion { get; set; }

            public string passwordAnswer { get; set; }

            public bool isApproved { get; set; }
            public List<string> Roles { get; set; }

        }
        public class ChangePasswordModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            [Obsolete]
            public string ConfirmPassword { get; set; }
        }

        public class LogOnModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class RolesModel
        {
            public string RoleName { get; set; }
        }

        public class CustomAuthFilter : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
                if (filterContext.HttpContext.Session["UserName"] == null || string.IsNullOrEmpty(filterContext.HttpContext.Session["UserName"].ToString()))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

                var action = filterContext.RequestContext.RouteData.GetRequiredString("action");
                var controller = filterContext.RequestContext.RouteData.GetRequiredString("controller");

                filterContext.HttpContext.Session["returnUrl"] = controller + "\\" + action;



            }

            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (filterContext.Result != null && filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action", "Logon" }, { "controller", "Identity" } });
                }
            }
        }
    }
