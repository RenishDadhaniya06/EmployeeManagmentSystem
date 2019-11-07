using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class ReportFilter
    {
        public string UserId { get; set; }

        public bool IsSuperAdmin { get; set; }
        //public WeekFilter WeekFilter { get; set; }
        public List<Report> Report { get; set; }
    }
}
