
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using EmployeeMangmentSystem.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Designation
    /// </summary>
    public class Designation
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Name", ResourceType = typeof(DesignationResources))]
        [Required]
        public string Name { get; set; }
    }
}
