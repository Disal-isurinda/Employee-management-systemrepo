using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcLeaveApply
    {
        public int LeaveID { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }

        [Display(Name = "Leave From ")]
        [Required]
        // [DataType(DataType.Date)]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public System.DateTime LeaveFrom { get; set; }
        public Nullable<System.DateTime> LeaveFrom { get; set; }

        [Required]
        [Display(Name = "Leave To ")]
        // [DataType(DataType.Date)]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public System.DateTime LeaveTo { get; set; }
        public Nullable<System.DateTime> LeaveTo { get; set; }


        [Display(Name = "Description ")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Leave Type ")]
        [Required]
        public int LeaveTypeID { get; set; }

        public string LeaveType1 { get; set; }

        [NotMapped]
        public IEnumerable<mvcLeaveType> ListOfLeaveTypes { get; set; }

        public virtual mvcEmployeeModel Employee { get; set; }
        public virtual mvcLeaveType LeaveType { get; set; }
    }
}