using System;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class DisplayCandidateViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int MobileNo { get; set; }

        public string Email { get; set; }

        public string CurrentCity { get; set; }

        public string Technology { get; set; }

        public string Skills { get; set; }

        public string Experience { get; set; }

        public string IntegerPart { get; set; }

        public string FractionPart { get; set; }

        public int CurrentSalary { get; set; }

        public int ExpectedSalary { get; set; }

        public int NoticePeriod { get; set; }

        public DateTime BirthDate { get; set; }

        public string Designation { get; set; }
    }
}
