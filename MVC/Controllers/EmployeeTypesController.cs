using MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeTypesController : Controller
    {
        // GET: Departments
        public ActionResult Index(int? page, string sortBy)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<mvcEmployeeTypeModel> pagedtypeList;
            IEnumerable<mvcEmployeeTypeModel> empTypeList;
            if (TempData["empTypesList"] != null)
            {
                empTypeList = TempData["empTypesList"] as IEnumerable<mvcEmployeeTypeModel>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("EmployeeTypes").Result;
                empTypeList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeTypeModel>>().Result;
            }
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.NameSortParm = sortBy == "Employee Type Name" ? "etname_desc" : "Employee Type Name";

            switch (sortBy)
            {
                case "id_desc":
                    empTypeList = empTypeList.OrderByDescending(e => e.EmployeeTypeID);
                    break;

                case "Employee Type Name":

                    empTypeList = empTypeList.OrderBy(e => e.EmployeeTypeName);

                    break;

                case "etname_desc":

                    empTypeList = empTypeList.OrderByDescending(e => e.EmployeeTypeName);
                    break;

                case "Default":
                    empTypeList = empTypeList.OrderBy(e => e.EmployeeTypeID);
                    break;
            }
            pagedtypeList = empTypeList.ToPagedList(pageIndex, pageSize);
            return View(pagedtypeList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcEmployeeTypeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("EmployeeTypes/" + id.ToString()).Result;
                //var list = new List<string>() { "Account", "HR", "Managing", "Develpment" };
                //ViewBag.list = list;
                return View(response.Content.ReadAsAsync<mvcEmployeeTypeModel>().Result);

                // return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeTypeModel empType)
        {

            if (ModelState.IsValid == true)

                if (empType.EmployeeTypeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("EmployeeTypes", empType).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("EmployeeTypes/" + empType.EmployeeTypeID, empType).Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("EmployeeTypes/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}