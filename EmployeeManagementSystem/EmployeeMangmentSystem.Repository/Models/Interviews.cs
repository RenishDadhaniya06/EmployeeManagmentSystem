using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Interviews
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid Technology { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public Guid Designation { get; set; }
    }
}
