
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using Helpers;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// DashboardController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class DashboardController : Controller
    {
        #region Index Method
        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<DashboardCounts>("api/Dashboard/DashboardCounts");
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}