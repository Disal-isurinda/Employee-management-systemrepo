namespace EmployeeManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MVC.Models;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<mvcDepartmentModel> Departments { get; set; }
        public virtual DbSet<mvcEmployeeModel> Employees { get; set; }
        public virtual DbSet<mvcLeaveApply> Leave_Type { get; set; }
        public virtual DbSet<mvcLeaveType> Leaves { get; set; }
       // public virtual DbSet<Role> Roles { get; set; }
       // public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mvcDepartmentModel>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<mvcDepartmentModel>()
                .Property(e => e.DeptDescription)
                .IsUnicode(false);

            modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.NIC)
                .IsUnicode(false);

            // modelBuilder.Entity<mvcEmployeeModel>()
            //  .Property(e => e.MobileNum)
            //.ToString (false);

            modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.Address)
                .IsUnicode(false);

            /* modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.EmployeeTypeID)
                .IsUnicode(false);

             modelBuilder.Entity<mvcEmployeeModel>()
                .Property(e => e.DeptID)
                .IsUnicode(false);*/

            /// modelBuilder.Entity<mvcLeaveApply>()
            ///   .Property(e => e.LeaveFrom)
            /// .ToString (false);

            /// modelBuilder.Entity<mvcLeaveApply>()
            ///    .Property(e => e.LeaveTo)
            ///    .ToString (false);

            modelBuilder.Entity<mvcLeaveApply>()
               .Property(e => e.Description)
               .IsUnicode(false);

           

            modelBuilder.Entity<mvcLeaveType>()
                .Property(e => e.LeaveType1)
                .IsUnicode(false);

            
        }
    }
}
