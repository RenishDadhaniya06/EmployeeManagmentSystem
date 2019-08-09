using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class FilterViewModel
    {
        public IEnumerable<LeaveViewModel> Leaves { get; set; }

        public LeaveFilter LeaveFilters { get; set; }
    }
}
