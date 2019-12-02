using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Helpers.Enums;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class ResourceViewModel
    {
        public IEnumerable<EmployeeUserViewModel> EmployeeUserViewModels { get; set; }

        [Display(Name ="Select Skill")]
        public Guid Resource { get; set; }

        [Display(Name ="Currently Working")]
        public IsCurrentlyWorking IsCurrentlyWorking { get; set; }
    }
}
