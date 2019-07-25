using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal AvailableLeaves { get; set; }
        public Guid ReportingManagerId { get; set; }
    }
}
