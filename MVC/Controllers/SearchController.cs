using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult Index(String sQuery)
        {
            TempData["sQuery"] = null;
            Uri uriaddress = new Uri(Request.UrlReferrer.ToString());
            string page = uriaddress.Segments[1].Replace("/", "");
            if (!string.IsNullOrEmpty(sQuery))
            {
                TempData["sQuery"] = sQuery;

                if (page == "Employees")
                {
                    IEnumerable<mvcEmployeeModel> empList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Search/GetEmployee/" + sQuery).Result;
                    empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
                    TempData["empList"] = empList;
                    return RedirectToAction("Index", "Employees");
                }
                else if (page == "Departments")
                {
                    IEnumerable<mvcDepartmentModel> depList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Search/GetDepartment/" + sQuery).Result;
                    depList = response.Content.ReadAsAsync<IEnumerable<mvcDepartmentModel>>().Result;
                    TempData["depList"] = depList;
                    return RedirectToAction("Index", "Departments");
                }
            }
            return RedirectToAction("Index", page);
        }
    }
}