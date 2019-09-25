
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System;
    using static Helpers.Enums;
    #endregion


    /// <summary>
    /// LeaveViewModel
    /// </summary>
    public class LeaveViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
