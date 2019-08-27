using System;
using System.ComponentModel.DataAnnotations;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Interviews
    {
        public Guid Id { get; set; }

        [Display(Name = "Select Candidate")]
        public Guid CandidateId { get; set; }

        [Display(Name = "Select Employee")]
        public Guid EmployeeId { get; set; }

        //public Guid Designation { get; set; }

        //public string Technology { get; set; }

        //public string Skills { get; set; }

        public DateTime ScheduleTime { get; set; }

        public InterviewStatus InterviewStatus { get; set; }
    }
}
