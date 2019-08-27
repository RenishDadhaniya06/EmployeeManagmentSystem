using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class DisplayInterviewModel
    {
        public Guid Id { get; set; }

        public String Candidate { get; set; }

        public String Employee { get; set; }

        public DateTime ScheduleTime { get; set; }

        public InterviewStatus InterviewStatus { get; set; }
    }
}
