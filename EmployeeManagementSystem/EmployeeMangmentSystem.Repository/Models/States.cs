
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// States
    /// </summary>
    public class States
    {
        public Guid Id { get; set; }

        public Guid CountryId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
