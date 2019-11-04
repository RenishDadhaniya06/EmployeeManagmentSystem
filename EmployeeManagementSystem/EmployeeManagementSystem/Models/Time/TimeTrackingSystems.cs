using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.Time
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