using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Candidates
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Mobile Number")]
        //[DataType(DataType.PhoneNumber)]
        //[Required(ErrorMessage = "Mobile Number Required")]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile")]
        //public string MobileNo { get; set; }
        //[StringLength(10,MinimumLength =10)]
        public int MobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Current City")]
        public string CurrentCity { get; set; }

        [Display(Name = "Experience")]
        public decimal Experience { get; set; }

        [Display(Name = "Current Salary")]
        public int CurrentSalary { get; set; }

        [Display(Name = "Expected Salary")]
        public int ExpectedSalary { get; set; }

        [Display(Name = "Notice Period")]
        public int NoticePeriod { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public Guid Designation { get; set; }
    }
}
