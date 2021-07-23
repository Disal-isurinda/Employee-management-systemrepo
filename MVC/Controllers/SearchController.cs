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
        public ActionResult Index(String sQuery)
        {
            string page = Request.UrlReferrer.AbsolutePath.ToString().Replace("/", "");
            if (!string.IsNullOrEmpty(sQuery))
            {
                sQuery = sQuery.ToLower();
                var isQuery = -1;
                Int32.TryParse(sQuery, out isQuery);
                if (page == "Employees")
                {
                    IEnumerable<mvcEmployeeModel> empList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
                    empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

                    empList = empList.Where(e => e.EmpID.Equals(isQuery) || e.FirstName.ToLower().Contains(sQuery));
                    TempData["empList"] = empList;
                    return RedirectToAction("Index", "Employees");
                }
                else if (page == "Departments")
                {
                    IEnumerable<mvcDepartmentModel> depList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Departments").Result;
                    depList = response.Content.ReadAsAsync<IEnumerable<mvcDepartmentModel>>().Result;

                    depList = depList.Where(d => d.DeptID.Equals(isQuery) || d.DeptName.ToLower().Contains(sQuery));
                    TempData["depList"] = depList;
                    return RedirectToAction("Index", "Departments");
                }
            }
            return RedirectToAction("Index", page);
        }
    }
}