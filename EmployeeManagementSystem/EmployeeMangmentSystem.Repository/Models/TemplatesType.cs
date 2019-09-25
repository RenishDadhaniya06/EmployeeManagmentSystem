
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// TemplatesType
    /// </summary>
    public class TemplatesType
    {
        public Guid Id { get; set; }

        //[Required]
        [Display(Name ="Name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
