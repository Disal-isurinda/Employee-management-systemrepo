using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {
            if (userName == "admin" && userPassword == "admin")
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(userName));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Username and password is invalid");
            }
        }
        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Valid");
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}