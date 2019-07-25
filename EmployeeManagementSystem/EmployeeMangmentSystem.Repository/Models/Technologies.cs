using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Technologies
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
