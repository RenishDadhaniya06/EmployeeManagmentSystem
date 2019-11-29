using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers.api
{
    public class ResourceController : ApiController
    {
        private ICustomerService _customerService;

        public ResourceController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("api/Resource/GetAvailableResources/{id}")]
        public async Task<List<EmployeeUserViewModel>> GetAvaliableResources(Guid id)
        {
            try
            {
                var data = await _customerService.GetAvailableResources(id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
