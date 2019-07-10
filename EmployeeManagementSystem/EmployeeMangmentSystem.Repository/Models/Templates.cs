using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Templates
    {
        [Key]
        public Guid Id { get; set; }

        //[Required]
        [Display(Name ="Name")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "TemplateType")]
        public Guid TemplateType { get; set; }

        //[Required]
        [Display(Name ="Content")]
        public string TemplateContent { get; set; }

        public bool IsActive { get; set; }
    }
}
