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
using static WebApi.Models.MembershipProvider;

namespace WebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private DBModel db = new DBModel();
        [CustomAuthFilter]
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
            string password ="ab@1234";
            MembershipCreateStatus memcresta;
            MembershipUser user = Membership.CreateUser(username, password, email, "question", "answer", true, out memcresta);
            var User = Membership.GetUser(username);
           // var currentUser = Membership.ValidateUser(username, password);
            //string[] Roles = System.Web.Security.Roles.GetAllRoles();
            //List<string> RolesList = new List<string>();
            //foreach (var role in Roles)
            //{
            //    if (HttpContext.User.IsInRole("Admin"))
            //    {
            //        RolesList.Add(role);
            //    }
            //    else
            //    {
            //        if (role != "Admin")
            //        {
            //            RolesList.Add(role);
            //        }
            //    }


            //}
            //UsersModel usersModel = new UsersModel();
            //usersModel.Roles = RolesList;

            return CreatedAtRoute("DefaultApi", new { id = employee.EmpID }, employee);
        }

        [CustomAuthFilter]
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
        [HttpGet]
        //GET- Retrive Data
        //[ResponseType(typeof(User))]
        [Route("api/Employees/GetValidateUser/{username}/{password}")]
        public IHttpActionResult GetValidateUser(string username, string password)
        {
            User user = new User();
            user.UserName = username;
            user.Password = password;
            DBModel db = new DBModel();
            // var result = db.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
            var result = Membership.ValidateUser(username, password);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //var result = db.Users.Where(Membership.GetUser => .username == username && x.password == password).FirstOrDefault();
        //var currentUser = (Membership.ValidateUser(username, password));
        //var currentUser = Membership.ValidateUser(user.UserName, user.Password) ;
            // return Ok(currentUser );
            //return Ok(user);
        
    }


}