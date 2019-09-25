
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System;
    using static Helpers.Enums;
    #endregion


    /// <summary>
    /// DisplayInterviewModel
    /// </summary>
    public class DisplayInterviewModel
    {
        public Guid Id { get; set; }

        public string Candidate { get; set; }

        public string Employee { get; set; }

        public DateTime ScheduleTime { get; set; }

        public InterviewStatus InterviewStatus { get; set; }
    }
}
