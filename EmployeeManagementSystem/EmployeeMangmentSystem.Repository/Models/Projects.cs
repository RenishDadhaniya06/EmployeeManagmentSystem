
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Helpers.Enums;
    #endregion


    /// <summary>
    /// Projects
    /// </summary>
    public class Projects
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Project Status")]
        public ProjectStatus ProjectStatus { get; set; }

        [Display(Name = "Project Type")]
        public ProjectType ProjectType { get; set; }

        public string Documents { get; set; }
    }
}
