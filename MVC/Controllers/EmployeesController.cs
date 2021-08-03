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
        public ActionResult Index(int? page, string sortBy)
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
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.FNameSortParm = sortBy == "First Name" ? "fname_desc" : "First Name";

            switch (sortBy)
            {
                case "id_desc":
                    empList = empList.OrderByDescending(e => e.EmpID);
                    break;

                case "First Name":

                    empList = empList.OrderBy(e => e.FirstName);

                    break;

                case "fname_desc":

                    empList = empList.OrderByDescending(e => e.FirstName);
                    break;

                case "Default":
                    empList = empList.OrderBy(e => e.EmpID);
                    break;
            }
            pagedempList = empList.ToPagedList(pageIndex, pageSize);
            return View(pagedempList);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrEdit(int id = 0)
        {
            var responseto = await GlobalVariables.WebApiClient.GetAsync("Departments").Result.Content.ReadAsAsync<IList<mvcDepartmentModel>>();

            responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            // var dataObjects = JsonConvert.DeserializeObject<AddressList>(json)
            ViewBag.list = responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            //------------------------------------

            var response3 = await GlobalVariables.WebApiClient.GetAsync("EmployeeTypes").Result.Content.ReadAsAsync<IList<mvcEmployeeTypeModel>>();

            response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });

            ViewBag.list2 = response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });


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
            if (ModelState.IsValid == true)

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