
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// Employee
    /// </summary>
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal AvailableLeaves { get; set; }
        public Guid ReportingManagerId { get; set; }
    }
}
