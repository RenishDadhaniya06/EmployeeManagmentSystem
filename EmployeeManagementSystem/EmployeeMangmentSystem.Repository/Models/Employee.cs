
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Helpers.Enums;
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

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Mobile Number is not valid")]
        //[RegularExpression(@"^[1-9]\d{10}$", ErrorMessage = "Mobile Number is not valid")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="Mobile number is not valid")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-mm-yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Other Contact")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        //[RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Mobile Number is not valid")]
        public string OtherContact { get; set; }

        [Required]
        public Guid Department { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Current Salary")]
        [RegularExpression(@"^[1-9]{1}[0-9]{3,10}$",ErrorMessage ="Current Salary is not valid")]
        public string CurrentSalary { get; set; }

        [Required]
        [Display(Name = "Leave Balance")]
        //[RegularExpression(@"^[0-1]{1}[0-9]{1}$", ErrorMessage = "Leave Balance is not valid")]
        public string LeaveBalance { get; set; }

        public decimal Experience { get; set; }
        //[Experience]

        [Required]
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-mm-yyyy}")]
        public DateTime JoiningDate { get; set; }

        public bool IsEmailVerified { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Display(Name ="Blood Group")]
        public BloodGroups BloodGroup { get; set; }

        [Required]
        [Display(Name ="Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Required]
        [Display(Name ="Emergency Contact Phone")]
        [DataType(DataType.PhoneNumber)]
        //[Phone(ErrorMessage = "Mobile Number is not valid")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Mobile number is not valid")]
        public string EmergencyContactNumber { get; set; }
    }
}
