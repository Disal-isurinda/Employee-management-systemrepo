using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcLeaveApply
    {
        public int LeaveID { get; set; }
        public int EmpID { get; set; }
        public System.DateTime LeaveFrom { get; set; }
        public System.DateTime LeaveTo { get; set; }
        public string Description { get; set; }
        public int LeaveTypeID { get; set; }

        public virtual mvcEmployeeModel Employee { get; set; }
        public virtual mvcLeaveType LeaveType { get; set; }
    }
}