
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Departments
    /// </summary>
    public class Departments
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
