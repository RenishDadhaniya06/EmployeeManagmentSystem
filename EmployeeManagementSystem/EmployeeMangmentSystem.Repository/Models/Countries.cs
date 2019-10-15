
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Countries
    /// </summary>
    public class Countries
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
