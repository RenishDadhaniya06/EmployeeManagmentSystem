
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// City
    /// </summary>
    public class City
    {
        public Guid Id { get; set; }

        public Guid StateId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}
