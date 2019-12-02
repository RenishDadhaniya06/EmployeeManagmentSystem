
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Employee
    /// </summary>
    public class Employee
    {
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public decimal AvailableLeaves { get; set; }
        //public Guid ReportingManagerId { get; set; }

        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Other Contact")]
        [DataType(DataType.PhoneNumber)]
        public string OtherContact { get; set; }

        public Guid Department { get; set; }


        [Display(Name = "Current Salary")]

        public string CurrentSalary { get; set; }

        [Display(Name = "Leave Balance")]

        public string LeaveBalance { get; set; }

        public bool IsEmailVerified { get; set; }

        public Guid UserId { get; set; }
    }
}
