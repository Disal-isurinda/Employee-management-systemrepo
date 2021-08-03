using MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class LeaveApplyController : Controller
    {
        // GET: LeaveApply
        public ActionResult Index()
        {
            // mvcLeaveType A = new mvcLeaveType();
            //ViewBag.typeList=new SelectList(A.ListOfLeaveTypes, "LeaveTypeID", "LeaveType1");

            IEnumerable<mvcLeaveApply> empLeaveList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("LeaveApply").Result;
            empLeaveList = response.Content.ReadAsAsync<IEnumerable<mvcLeaveApply>>().Result;

            return View(empLeaveList);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrEdit(int id = 0)
        {
            var responseto = await GlobalVariables.WebApiClient.GetAsync("LeaveTypes").Result.Content.ReadAsAsync<IList<mvcLeaveType>>();

            responseto.Select(d => new SelectListItem { Text = d.LeaveType1, Value = d.LeaveTypeID.ToString() });

            ViewBag.list = responseto.Select(d => new SelectListItem { Text = d.LeaveType1, Value = d.LeaveTypeID.ToString() });

            if (id == 0)
            {
                return View(new mvcLeaveApply());
            }
            else
            {
                var response = await GlobalVariables.WebApiClient.GetAsync("LeaveApply/" + id.ToString()).Result.Content.ReadAsAsync<mvcLeaveApply>();

                return View(response);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcLeaveApply leave)
        {
            if (leave.LeaveID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("LeaveApply", leave).Result;
                HttpResponseMessage mailresponse = GlobalVariables.WebApiClient.PostAsJsonAsync("Email/PostLeaveApplyMail", leave).Result;

                TempData["SuccessMessage"] = "Leave Requested Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("LeaveApply/" + leave.LeaveID, leave).Result;
                TempData["SuccessMessage"] = "Update Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("LeaveApply/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}