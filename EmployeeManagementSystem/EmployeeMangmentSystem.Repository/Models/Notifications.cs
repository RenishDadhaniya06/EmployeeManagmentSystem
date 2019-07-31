using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Notifications
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        //public string Duration { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}
