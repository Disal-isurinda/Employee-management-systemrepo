using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebApi.Models
{
    public class MembershipProvider
    {
        public class UsersModel
        {
            public string username { get; set; }

            public string password { get; set; }

            public string email { get; set; }
            //public int EmpID { get; set; }
            public string passwordQuestion { get; set; }

            public string passwordAnswer { get; set; }

            public bool isApproved { get; set; }
            public List<string> Roles { get; set; }

        }

        public class RolesModel
        {
            public string RoleName { get; set; }
        }
      
    }
}