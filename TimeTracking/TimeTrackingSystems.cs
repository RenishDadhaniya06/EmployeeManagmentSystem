using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.TimeTracking
{
    public class TimeTrackingSystems
    {
        public Guid id { get; set; }

        public Guid UserId { get; set; }

        public string Date { get; set; }

        public bool IsClockIn { get; set; }

        public bool IsBreakIn { get; set; }
    }
}
