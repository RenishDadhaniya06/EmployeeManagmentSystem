using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.Time
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