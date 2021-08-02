using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class aspnet_UsersController : Controller
    {
        // GET: aspnet_Users
        public ActionResult Index()
        {
            return View();
        }
    }
}