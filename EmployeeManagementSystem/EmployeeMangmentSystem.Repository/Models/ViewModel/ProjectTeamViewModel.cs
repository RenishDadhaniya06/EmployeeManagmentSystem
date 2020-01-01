
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    using System.Collections.Generic;
    #region Using
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion


    /// <summary>
    /// ProjectTeamViewModel
    /// </summary>
    /// <seealso cref="EmployeeMangmentSystem.Repository.Models.Projects" />
    [NotMapped]
    public class ProjectTeamViewModel :  Projects
    {
        [Display(Name ="Select Employees")]
        public string EmployeeId { get; set; }

        public List<TeamViewModel> Employees { get; set; }
    }
    [NotMapped]
    public class TeamViewModel : Employee {

        [Display(Name = "Currently Working")]
        public bool CurrentlyWorking { get; set; }
        public string Skills { get; set; }
        public string ProjectName { get; set; }
    }
}
