
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using static Helpers.Enums;
    #endregion


    /// <summary>
    /// Leave
    /// </summary>
    public class Leave
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string AssignTo { get; set; }
        public LeaveType LeaveType { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString ="{DD-MM-YYYY}")]
        public DateTime From { get; set; }

        //[DisplayFormat(DataFormatString ="{DD-MM-YYYY")]
        public DateTime To { get; set; }
    }
}
