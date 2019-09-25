
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// CandidateSkills
    /// </summary>
    public class CandidateSkills
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public Guid SkillId { get; set; }
    }
}
