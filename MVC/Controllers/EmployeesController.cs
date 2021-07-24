using MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index(int? page, string sortBy, string sortOrder, string psortBy)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<mvcEmployeeModel> pagedempList;
            IEnumerable<mvcEmployeeModel> empList;
            if (TempData["empList"] != null)
            {
                empList = TempData["empList"] as IEnumerable<mvcEmployeeModel>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
                empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
            }

            sortOrder = (string.IsNullOrWhiteSpace(sortOrder) || sortOrder.Equals("asc")) ? "desc" : "asc";

            if (!string.IsNullOrWhiteSpace(sortBy) && !sortBy.Equals(psortBy, StringComparison.CurrentCultureIgnoreCase))
            {
                sortOrder = "asc";
            }
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortBy = sortBy;
            sortBy = String.IsNullOrEmpty(sortBy) ? "Employee ID" : sortBy;
            switch (sortBy)
            {
                case "Employee ID":
                    if (sortOrder.Equals("desc"))
                    {
                        empList = empList.OrderByDescending(e => e.EmpID);
                    }
                    else
                    {
                        empList = empList.OrderBy(e => e.EmpID);
                    }
                    break;

                case "First Name":
                    if (sortOrder.Equals("desc"))
                    {
                        empList = empList.OrderByDescending(e => e.FirstName);
                    }
                    else
                    {
                        empList = empList.OrderBy(e => e.FirstName);
                    }
                    break;

                case "Default":
                    empList = empList.OrderBy(e => e.EmpID);
                    break;
            }
            pagedempList = empList.ToPagedList(pageIndex, pageSize);
            return View(pagedempList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcEmployeeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
                //var list = new List<string>() { "Account", "HR", "Managing", "Develpment" };
                //ViewBag.list = list;
                return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
                // return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeModel emp)
        {
            if (emp.EmpID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees", emp).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/" + emp.EmpID, emp).Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employees/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}