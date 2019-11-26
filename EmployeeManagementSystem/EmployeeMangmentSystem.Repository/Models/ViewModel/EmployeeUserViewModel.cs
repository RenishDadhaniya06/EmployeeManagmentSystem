using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    [NotMapped]
    public class EmployeeUserViewModel : Employee
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Skills { get; set; }
    }
}
