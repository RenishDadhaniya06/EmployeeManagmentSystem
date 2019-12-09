
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// DashboardController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DashboardController : ApiController
    {
        private ICustomerService _customerService;

        public DashboardController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("api/Dashboard/DashboardCounts")]
        [HttpGet]
        public async Task<DashboardCounts> DashboardCounts()
        {
            try
            {
                return await _customerService.GetDashboardCounts();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Dashboard/GetMonthBirthdays")]
        [HttpGet]
        public async Task<List<MonthBirthdays>> GetMonthBirthdays()
        {
            try
            {
                return await _customerService.GetMonthBirthdays();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
