
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

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name = "Project Status")]
        public ProjectStatus ProjectStatus { get; set; }

        [Display(Name = "Project Type")]
        public ProjectType ProjectType { get; set; }

        public string Documents { get; set; }
    }
}
