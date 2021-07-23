﻿using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using PagedList;

namespace MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Departments
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? System.Convert.ToInt32(page) : 1;
            IPagedList<mvcDepartmentModel> pageddepList;
            IEnumerable<mvcDepartmentModel> depList;
            if (TempData["depList"] != null)
            {
                depList = TempData["depList"] as IEnumerable<mvcDepartmentModel>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Departments").Result;
                depList = response.Content.ReadAsAsync<IEnumerable<mvcDepartmentModel>>().Result;
            }
            pageddepList = depList.ToPagedList(pageIndex, pageSize);
            return View(pageddepList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcDepartmentModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Departments/" + id.ToString()).Result;
                //var list = new List<string>() { "Account", "HR", "Managing", "Develpment" };
                //ViewBag.list = list;
                return View(response.Content.ReadAsAsync<mvcDepartmentModel>().Result);

                // return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcDepartmentModel dep)
        {
            if (dep.DeptID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Departments", dep).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Departments/" + dep.DeptID, dep).Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Departments/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}