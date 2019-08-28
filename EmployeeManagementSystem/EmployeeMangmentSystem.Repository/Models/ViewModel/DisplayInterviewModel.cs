using System;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class DisplayInterviewModel
    {
        public Guid Id { get; set; }

        public string Candidate { get; set; }

        public string Employee { get; set; }

        public DateTime ScheduleTime { get; set; }

        public InterviewStatus InterviewStatus { get; set; }
    }
}
