﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class CandidateViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Mobile Number")]
        public int MobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Current City")]
        public string CurrentCity { get; set; }

        [Display(Name = "Technology")]
        public string Technology { get; set; }

        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Display(Name = "Experience")]
        public decimal Experience { get; set; }

        [Display(Name = "Current Salary")]
        public int CurrentSalary { get; set; }

        [Display(Name = "Expected Salary")]
        public int ExpectedSalary { get; set; }

        [Display(Name = "Notice Period")]
        public int NoticePeriod { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public Guid Designation { get; set; }
    }
}
