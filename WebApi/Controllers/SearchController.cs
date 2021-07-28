using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SearchController : ApiController
    {
        private DBModel db = new DBModel();

        [Route("api/Search/GetEmployee/{sQuery}")]
        public IHttpActionResult GetEmployee(string sQuery)
        {
            var isQuery = -1;
            Int32.TryParse(sQuery, out isQuery);
            var employee = db.EmpSearchF(isQuery, sQuery).ToList();
            return Ok(employee);
        }

        [Route("api/Search/GetDepartment/{sQuery}")]
        public IHttpActionResult GetDepartment(string sQuery)
        {
            var isQuery = -1;
            Int32.TryParse(sQuery, out isQuery);
            var department = db.DeptSearchF(isQuery, sQuery).ToList();
            return Ok(department);
        }
    }
}