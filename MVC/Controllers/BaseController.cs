using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {

        public int UserID { get; set; }
        public string UserName { get; set; }
        // GET: Base
        //public ActionResult Index()
        //{

        //    return View();
        //}
    }
}