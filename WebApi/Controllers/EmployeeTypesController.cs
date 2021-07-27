using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeTypesController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/EmployeeTypes
        [HttpGet]
        public List<EmployeeType> GetDepartments()
        {
            List<EmployeeType> EmpTypeList = new List<EmployeeType>();
            EmpTypeList = (from EmployeeType in db.EmployeeTypes select EmployeeType).ToList();
            return EmpTypeList;
        }

        // GET: api/EmployeeTypes/5
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult GetEmployeeType(int id)
        {
            EmployeeType employeeType = db.EmployeeTypes.Find(id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return Ok(employeeType);
        }

        // PUT: api/EmployeeTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeType(int id, EmployeeType employeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeType.EmployeeTypeID)
            {
                return BadRequest();
            }

            db.Entry(employeeType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeTypes
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult PostEmployeeType(EmployeeType employeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeTypes.Add(employeeType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeType.EmployeeTypeID }, employeeType);
        }

        // DELETE: api/EmployeeTypes/5
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult DeleteEmployeeType(int id)
        {
            EmployeeType employeeType = db.EmployeeTypes.Find(id);
            if (employeeType == null)
            {
                return NotFound();
            }

            db.EmployeeTypes.Remove(employeeType);
            db.SaveChanges();

            return Ok(employeeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeTypeExists(int id)
        {
            return db.EmployeeTypes.Count(e => e.EmployeeTypeID == id) > 0;
        }
    }
}