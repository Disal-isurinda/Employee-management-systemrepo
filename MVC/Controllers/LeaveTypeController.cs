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
    public class LeaveTypeController : BaseController
    {

        // GET: Departments
        public ActionResult Index(int? page, string sortBy)
        {

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<mvcLeaveType> pagedtypeList;
            IEnumerable<mvcLeaveType> leaveTypeList;
            if (TempData["leaveTypesList"] != null)
            {
                leaveTypeList = TempData["leaveTypesList"] as IEnumerable<mvcLeaveType>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("LeaveTypes").Result;
                leaveTypeList = response.Content.ReadAsAsync<IEnumerable<mvcLeaveType>>().Result;
            }
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.NameSortParm = sortBy == "Leave Name" ? "etname_desc" : "Leave Name";

            switch (sortBy)
            {
                case "id_desc":
                    leaveTypeList = leaveTypeList.OrderByDescending(e => e.LeaveTypeID);
                    break;

                case "Leave Name":

                    leaveTypeList = leaveTypeList.OrderBy(e => e.LeaveType1);

                    break;

                case "etname_desc":

                    leaveTypeList = leaveTypeList.OrderByDescending(e => e.LeaveType1);
                    break;

                case "Default":
                    leaveTypeList = leaveTypeList.OrderBy(e => e.LeaveTypeID);
                    break;
            }
            pagedtypeList = leaveTypeList.ToPagedList(pageIndex, pageSize);
            return View(pagedtypeList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (Response.StatusCode.Equals(400))
            {

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("LeaveTypes/").Result;
            }
            if (id == 0)
            {
                return View(new mvcLeaveType());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("LeaveTypes/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<mvcLeaveType>().Result);

                // return View();
            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcLeaveType empType)
        {

            if (empType.LeaveTypeID == 0)
            {
                //ViewData["EmpID"] = 3;

                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("LeaveTypes", empType).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Leave Type Added";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //ViewData["EmpID"] = 3;

                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("LeaveTypes/" + empType.LeaveTypeID, empType).Result;
                TempData["SuccessMessage"] = "Update Successfully";
            }
            ModelState.AddModelError(string.Empty, "Already Added.");

            return View();

        }


        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("LeaveTypes/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }

}