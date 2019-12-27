
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System;
    using System.Collections.Generic;
    #endregion


    /// <summary>
    /// DashboardCounts
    /// </summary>
    public class DashboardCounts
    {
        public int TotalDeveloper { get; set; }

        public int TotalHR { get; set; }

        public int TotalPM { get; set; }

        public int TotalSales { get; set; }

        public int TotalEmployee { get; set; }

        public int TotalProjects { get; set; }
    }

    /// <summary>
    /// MonthBirthdays
    /// </summary>
    public class MonthBirthdays
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }

    /// <summary>
    /// DashboardViewModel
    /// </summary>
    /// <seealso cref="EmployeeMangmentSystem.Repository.Models.ViewModel.DashboardCounts" />
    public class DashboardViewModel : DashboardCounts
    {
        public List<MonthBirthdays> MonthBirthdays { get; set; }
    }
}
