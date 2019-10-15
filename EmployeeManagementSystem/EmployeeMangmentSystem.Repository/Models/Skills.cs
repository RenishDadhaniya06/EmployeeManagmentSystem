
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Skills
    /// </summary>
    public class Skills
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
