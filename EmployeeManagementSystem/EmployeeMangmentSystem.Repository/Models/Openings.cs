using System;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class Openings
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public Guid Technology { get; set; }

        public decimal Experience { get; set; }

        public string Skills { get; set; }
        
        public Guid Department { get; set; }

        public string Description { get; set; }

        public OpeningStatus Openingstatus { get; set; }
    }
}
