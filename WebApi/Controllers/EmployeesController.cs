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
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web;

namespace WebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {


            if (id != employee.EmpID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {


            db.Employees.Add(employee);
            db.SaveChanges();
            string username = employee.FirstName + "." + employee.LastName;
            string email = username + "@gmail.com";
            string password = "ab@1234";
            MembershipCreateStatus memcresta;
            MembershipUser user = Membership.CreateUser(username, password, email, "question", "answer", true, out memcresta);
            var User = Membership.GetUser(username);
            var currentUser = Membership.ValidateUser(username, password);
     
            return CreatedAtRoute("DefaultApi", new { id = employee.EmpID }, employee);
        }
      

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmpID == id) > 0;
        }
        //GET- Retrive Data
        public IHttpActionResult GetValidateUser(string username, string password)
        {
            //DBModel db = new DBModel();
            //var result = db.Users.Where(Membership.GetUser => .username == username && x.password == password).FirstOrDefault();
            //var currentUser = (Membership.ValidateUser(username, password));
            var currentUser = Membership.ValidateUser(username, password) ;
            return Ok(currentUser );

            //if (result != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }

}