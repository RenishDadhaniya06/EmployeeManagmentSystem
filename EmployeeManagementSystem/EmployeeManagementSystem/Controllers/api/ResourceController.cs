
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
    /// ResourceController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ResourceController : ApiController
    {
        private ICustomerService _customerService;

        public ResourceController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("api/Resource/GetAvailableResources")]
        public async Task<List<EmployeeUserViewModel>> GetAvaliableResources(Guid id,bool workingid)
        {
            try
            {
                var data = await _customerService.GetAvailableResources(id,workingid);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
