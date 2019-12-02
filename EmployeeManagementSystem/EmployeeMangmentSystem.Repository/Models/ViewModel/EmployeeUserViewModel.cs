﻿
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    using System;
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
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>
        /// The skills.
        /// </value>
        public string Skills { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public string RoleId { get; set; }
    }
}
