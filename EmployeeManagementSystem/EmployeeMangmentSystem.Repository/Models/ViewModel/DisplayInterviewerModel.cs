
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// DisplayInterviewerModel
    /// </summary>
    public class DisplayInterviewerModel
    {
        public Guid Id { get; set; }

        public string Employee { get; set; }

        public string Designation { get; set; }

        public string Technology { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
