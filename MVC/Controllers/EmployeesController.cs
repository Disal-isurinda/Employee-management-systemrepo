using EmployeeManagementSystem.Models;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private mvcDepartmentModel db = new mvcDepartmentModel();


        // GET: Employees
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
            return View(empList);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrEdit(int id = 0)
        {

            //// ViewBag.deptId = new SelectList(db.Departments, "DeptID", "DeptName");
            //// return View();
            var responseto = await GlobalVariables.WebApiClient.GetAsync("Departments").Result.Content.ReadAsAsync<IList<mvcDepartmentModel>>();

            responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            // var dataObjects = JsonConvert.DeserializeObject<AddressList>(json)
            ViewBag.list = responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            //------------------------------------

            var response3 = await GlobalVariables.WebApiClient.GetAsync("EmployeeTypes").Result.Content.ReadAsAsync<IList<mvcEmployeeTypeModel>>();

            response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });

            ViewBag.list2 = response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });


            if (id == 0)
            {
                // HttpResponseMessage responseto = GlobalVariables.WebApiClient.GetAsync("Departments").Result;
               
                

                return View(new mvcEmployeeModel());
            }
            else
            {
                var response = await GlobalVariables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result.Content.ReadAsAsync<mvcEmployeeModel>();
                // ViewBag.deptId = new SelectList(db.Departments, "DeptID", "DeptName");

                //var list = new List<string>() { "Account", "HR", "Managing", "Develpment" };
                // ViewBag.list = list;
                return View(response);

                // return View();
                /*  List<SelectListItem> ObjList = new List<SelectListItem>()
                  {
                        new SelectListItem { Text = "Accounting", Value = "1" },
                        new SelectListItem { Text = "Managing", Value = "2" },
                        new SelectListItem { Text = "HR", Value = "3" },
                        new SelectListItem { Text = "Developing", Value = "4" },

                  };
                  //Assigning generic list to ViewBag
                  ViewBag.Locations = ObjList;
               //// return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);

                  return View();*/
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