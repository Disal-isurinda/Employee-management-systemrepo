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

        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }

        [Display(Name = "Leave From ")]
        [Required(ErrorMessage = "Leave From is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime LeaveFrom { get; set; }

        [Required(ErrorMessage = "Leave to is required")]
        [Display(Name = "Leave To ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime LeaveTo { get; set; }

        [Display(Name = "Description ")]
        public string Description { get; set; }

        [Display(Name = "Leave Type ")]
        [Required(ErrorMessage = "Leave Type is required")]
        public int LeaveTypeID { get; set; }

        //[NotMapped]
       // public IEnumerable<mvcLeaveType> ListOfLeaveTypes { get; set; }

       // public virtual mvcEmployeeModel Employee { get; set; }
       // public virtual mvcLeaveType LeaveType { get; set; }

    }
}