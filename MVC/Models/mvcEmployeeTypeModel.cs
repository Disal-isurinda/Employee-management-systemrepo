using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcEmployeeTypeModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcEmployeeTypeModel()
        {
            this.Employees = new HashSet<mvcEmployeeModel>();
        }

        public int EmployeeTypeID { get; set; }
        public string EmployeeTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcEmployeeModel> Employees { get; set; }
    }
}