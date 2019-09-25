
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using static Helpers.Enums;
    #endregion


    /// <summary>
    /// LeaveFilter
    /// </summary>
    public class LeaveFilter
    {
        public string Name { get; set; }

        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }

        public LeaveType leavetype { get; set; }

        public LeaveStatus leavestatus { get; set; }
    }
}
