using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.TimeTracking
{
    public class TimeTrackings
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Date { get; set; }

        public string In { get; set; }

        public string Out { get; set; }

        public string CreatedDate { get; set; }
    }
}
