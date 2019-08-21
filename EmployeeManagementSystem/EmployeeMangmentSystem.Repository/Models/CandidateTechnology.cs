using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class CandidateTechnologies
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public Guid TechnologyId { get; set; }
    }
}
