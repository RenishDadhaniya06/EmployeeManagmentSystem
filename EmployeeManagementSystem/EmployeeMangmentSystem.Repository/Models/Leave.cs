using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Leave
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string AssignTo { get; set; }
        public LeaveType LeaveType { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
