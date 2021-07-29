using MVC.Models;
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
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeTypeModel> empTypeList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("EmployeeTypes").Result;
            empTypeList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeTypeModel>>().Result;
            return View(empTypeList);
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
