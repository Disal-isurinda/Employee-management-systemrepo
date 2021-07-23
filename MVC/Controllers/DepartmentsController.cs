using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Departments
        public ActionResult Index()
        {
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

            return View(depList);
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