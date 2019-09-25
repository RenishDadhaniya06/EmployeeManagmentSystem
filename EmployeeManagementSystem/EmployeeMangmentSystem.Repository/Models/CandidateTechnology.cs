
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// CandidateTechnologies
    /// </summary>
    public class CandidateTechnologies
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public Guid TechnologyId { get; set; }
    }
}
