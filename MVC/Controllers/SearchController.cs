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
            //Getting Request URLs to change the Search Functions Accrodingly
            Uri uriaddress = new Uri(Request.UrlReferrer.ToString());
            string page = uriaddress.Segments[1].Replace("/", "");
            if (!string.IsNullOrEmpty(sQuery))
            {
                //Keeping search Query for showing in the input box
                TempData["sQuery"] = sQuery;

                if (page == "Employees")
                {
                    IEnumerable<mvcEmployeeModel> empList;
                    //Getting the filtered results from the API
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
                else if (page == "EmployeeTypes")
                {
                    IEnumerable<mvcEmployeeTypeModel> depList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Search/GetEmpType/" + sQuery).Result;
                    depList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeTypeModel>>().Result;
                    TempData["empTypesList"] = depList;
                    return RedirectToAction("Index", "EmployeeTypes");
                }
            }
            return RedirectToAction("Index", page);
        }
    }
}