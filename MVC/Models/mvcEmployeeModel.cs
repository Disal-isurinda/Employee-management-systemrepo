using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcEmployeeModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcEmployeeModel()
        {
            this.LeaveApplies = new HashSet<mvcLeaveApply>();
        }

        public int EmpID { get; set; }
        [Required(ErrorMessage = "Enter FirstName")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter LastName")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter NIC")]

        public string NIC { get; set; }
        [Required(ErrorMessage = "Enter MobileNum")]

        public int MobileNum { get; set; }
        [Required(ErrorMessage = "Enter Address")]

        public string Address { get; set; }
        public int EmployeeTypeID { get; set; }
        public int DeptID { get; set; }
        [Required(ErrorMessage = "Enter Role")]

        public string Role { get; set; }


        public string DeptName { get; set; }
        public string EmployeeTypeName { get; set; }

        public virtual mvcDepartmentModel Department { get; set; }
        public virtual mvcEmployeeModel EmployeeType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcLeaveApply> LeaveApplies { get; set; }
        public virtual RolesModel Roles { get; set; }
    }
}