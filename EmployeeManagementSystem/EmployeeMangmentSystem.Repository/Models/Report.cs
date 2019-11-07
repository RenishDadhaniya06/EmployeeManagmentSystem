using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Report
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public TimeSpan TotalAge { get; set; }
    }
}
