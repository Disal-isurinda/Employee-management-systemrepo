using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public int MobileNum { get; set; }
        public string Address { get; set; }
        public string EmployeeTypeID { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string EmployeeTypeName { get; set; }




        public virtual mvcDepartmentModel Department { get; set; }
        public virtual mvcEmployeeModel EmployeeType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcLeaveApply> LeaveApplies { get; set; }
    }
}