using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class OpeningsViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string Technology { get; set; }

        public string Experience { get; set; }

        public string Leftside { get; set; }

        public string Rightside { get; set; }

        public string Skills { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }

        public OpeningStatus Openingstatus { get; set; }
    }
}
