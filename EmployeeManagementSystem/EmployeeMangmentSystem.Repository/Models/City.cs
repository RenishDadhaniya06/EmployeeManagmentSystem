using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class City
    {
        public Guid id { get; set; }

        public Guid StateId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
