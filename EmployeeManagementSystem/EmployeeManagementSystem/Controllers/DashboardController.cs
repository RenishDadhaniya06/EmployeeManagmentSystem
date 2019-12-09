
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// DashboardController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class DashboardController : Controller
    {
        #region Index Method
        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<DashboardCounts>("api/Dashboard/DashboardCounts");
                var temp = await APIHelpers.GetAsync<List<MonthBirthdays>>("api/Dashboard/GetMonthBirthdays");
                DashboardViewModel model = new DashboardViewModel
                {
                    TotalEmployee = data.TotalEmployee,
                    TotalDeveloper = data.TotalDeveloper,
                    TotalHR = data.TotalHR,
                    TotalPM = data.TotalPM,
                    TotalSales = data.TotalSales,
                    TotalProjects = data.TotalProjects,
                    MonthBirthdays = temp
                };
                //data.BirthDays = temp.BirthDays;
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public async Task<JsonResult> RoleWiseUserData()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<ChartViewModel>>("api/Dashboard/RoleWiseUser");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonResult> SkillWiseUserData()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<ChartViewModel>>("api/Dashboard/SkillWiseUsers");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}