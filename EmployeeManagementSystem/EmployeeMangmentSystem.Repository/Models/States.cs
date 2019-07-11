using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class States
    {
        public Guid Id { get; set; }

        public Guid CountryId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
