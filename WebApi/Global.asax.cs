using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Add these Lines to Serializing Data to JSON Format
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
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
