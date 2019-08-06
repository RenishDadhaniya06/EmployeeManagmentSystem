using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class LeaveViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
