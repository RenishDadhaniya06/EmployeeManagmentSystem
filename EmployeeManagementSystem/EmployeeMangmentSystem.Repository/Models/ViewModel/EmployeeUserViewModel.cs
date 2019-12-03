
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    #region Using
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion


    /// <summary>
    /// EmployeeUserViewModel
    /// </summary>
    /// <seealso cref="EmployeeMangmentSystem.Repository.Models.Employee" />
    [NotMapped]
    public class EmployeeUserViewModel : Employee
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        [Required]
        public string Skills { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public string RoleId { get; set; }

        public List<ProjectViewModel> Projects { get; set; }

    }

    [NotMapped]
    public class ProjectViewModel : Projects
    {
        [Display(Name = "Currently Working")]
        public bool CurrentlyWorking { get; set; }

        public Guid TeamId { get; set; }
    }
}
