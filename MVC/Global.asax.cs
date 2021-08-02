using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            if (!System.Web.Security.Roles.RoleExists("Admin"))
            {
                System.Web.Security.Roles.CreateRole("Admin");
            }

            if (!System.Web.Security.Roles.RoleExists("User"))
            {
                System.Web.Security.Roles.CreateRole("User");
            }

            if (Membership.GetUser("Admin") == null)
            {
                MembershipCreateStatus memCreSta;
                Membership.CreateUser("Admin", "Admin", "Admin@gmail.com", "Admin", "Admin", true, out memCreSta);
                if (memCreSta == MembershipCreateStatus.Success)
                {
                    System.Web.Security.Roles.AddUsersToRole(new[] { "Admin" }, "Admin");
                }
            }

        }
    }
}
