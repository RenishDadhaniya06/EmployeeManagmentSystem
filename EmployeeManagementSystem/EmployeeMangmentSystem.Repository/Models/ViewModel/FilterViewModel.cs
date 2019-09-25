
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System.Collections.Generic;
    #endregion


    /// <summary>
    /// FilterViewModel
    /// </summary>
    public class FilterViewModel
    {
        public IEnumerable<LeaveViewModel> Leaves { get; set; }

        public LeaveFilter LeaveFilters { get; set; }
    }
}
