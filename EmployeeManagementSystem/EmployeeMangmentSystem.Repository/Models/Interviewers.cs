
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// Interviewers
    /// </summary>
    public class Interviewers
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid Technology { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public Guid Designation { get; set; }
    }
}
