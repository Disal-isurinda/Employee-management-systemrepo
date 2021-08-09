using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebApi.Models
{
    public class MembershipProvider
    {
        public class UsersModel
        {
            public string username { get; set; }

            public string password { get; set; }

            public string email { get; set; }
            //public int EmpID { get; set; }
            public string passwordQuestion { get; set; }

            public string passwordAnswer { get; set; }

            public bool isApproved { get; set; }
            public List<string> Roles { get; set; }

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
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action", "LogOn" }, { "controller", "Identity" } });
                }
            }

        }
    }
}