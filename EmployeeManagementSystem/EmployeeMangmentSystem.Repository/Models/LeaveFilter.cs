using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class LeaveFilter
    {
        public string Name { get; set; }

        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }

        public LeaveType leavetype { get; set; }

        public LeaveStatus leavestatus { get; set; }
    }
}
