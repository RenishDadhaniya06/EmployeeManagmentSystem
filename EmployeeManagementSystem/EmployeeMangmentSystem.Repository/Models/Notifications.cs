
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// Notifications
    /// </summary>
    public class Notifications
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        //public string Duration { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}
