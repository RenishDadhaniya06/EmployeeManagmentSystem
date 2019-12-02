using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class ResourceViewModel
    {
        public IEnumerable<EmployeeUserViewModel> EmployeeUserViewModels { get; set; }

        public Guid Resource { get; set; }
    }
}
