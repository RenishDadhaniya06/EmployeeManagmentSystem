using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
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
