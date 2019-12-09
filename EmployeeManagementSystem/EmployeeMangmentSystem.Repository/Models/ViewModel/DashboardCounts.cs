using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    public class DashboardCounts
    {
        public int TotalDeveloper { get; set; }

        public int TotalHR { get; set; }

        public int TotalPM { get; set; }

        public int TotalSales { get; set; }

        public int TotalEmployee { get; set; }

        public int TotalProjects { get; set; }
    }

    public class MonthBirthdays
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }

    public class DashboardViewModel : DashboardCounts
    {
        public List<MonthBirthdays> MonthBirthdays { get; set; }
    }
}
