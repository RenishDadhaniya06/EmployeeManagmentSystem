
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// ProjectTeams
    /// </summary>
    public class ProjectTeams
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid UserId { get; set; }

        public bool CurrentlyWorking { get; set; }
    }
}
