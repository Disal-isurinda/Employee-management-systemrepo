using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcLeaveType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcLeaveType()
        {
            this.LeaveApplies = new HashSet<mvcLeaveApply>();
        }

        public int LeaveTypeID { get; set; }
        [Required(ErrorMessage = "please enter leave type")]
        public string LeaveType1 { get; set; }
        [Required(ErrorMessage = "please enter No of days")]
        public Nullable<int> NoOfDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcLeaveApply> LeaveApplies { get; set; }

        public List<mvcLeaveType> ListOfLeaveTypes { get; set; }
    }
}