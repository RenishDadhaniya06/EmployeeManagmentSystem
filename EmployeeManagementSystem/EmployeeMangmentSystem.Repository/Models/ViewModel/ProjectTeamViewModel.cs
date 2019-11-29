
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
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

        [Display(Name = "Currently Working")]
        public bool CurrentlyWorking { get; set; }
    }
}
