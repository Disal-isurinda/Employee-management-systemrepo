using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "CustomRoute1",
               url: "Add-or-Update-Employee",
               defaults: new { controller = "Employees", action = "AddOrEdit" }
           );

            routes.MapRoute(
                name: "CustomRoute2",
                url: "Add-or-Update-Type",
                defaults: new { controller = "EmployeeTypes", action = "AddOrEdit" }
            );

            routes.MapRoute(
                name: "CustomRoute3",
                url: "Add-or-Update-Leave",
                defaults: new { controller = "LeaveApply", action = "AddOrEdit" }
            );

            routes.MapRoute(
                name: "CustomRoute4",
                url: "Add-or-Update-Department",
                defaults: new { controller = "Departments", action = "AddOrEdit" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
