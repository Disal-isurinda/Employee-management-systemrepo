using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcDepartmentModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcDepartmentModel()
        {
            this.Employees = new HashSet<mvcEmployeeModel>();
        }

        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string DeptDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcEmployeeModel> Employees { get; set; }
    }
}