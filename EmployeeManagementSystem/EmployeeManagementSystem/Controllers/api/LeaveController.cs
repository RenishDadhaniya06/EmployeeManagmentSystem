using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace EmployeeManagementSystem.Controllers.api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class LeaveController : ApiController
    {
        /// <summary>
        /// The repository
        /// </summary>
        private IRepository<Leave> _repository;

        /// <summary>
        /// The i customer service
        /// </summary>
        private ICustomerService _iCustomerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="iCustomerService">The i customer service.</param>
        public LeaveController(IRepository<Leave> repository, ICustomerService iCustomerService)
        {
            _repository = repository;
            _iCustomerService = iCustomerService;
        }

        /// <summary>
        /// Gets the leaves by employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("api/Leave/GetLeavesByEmployee/{id}")]
        public async Task<List<Leave>> GetLeavesByEmployee(Guid id)
        {
            try
            {
                var data = await _iCustomerService.GetLeavesByEmployee(id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("api/Leave/Get/{id}")]
        [HttpGet]
        public Leave Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("api/Leave/Post")]
        [HttpPost]
        public Leave Post(Leave model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        /// <summary>
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("api/Leave/Put")]
        [HttpPut]
        public Leave Put(Leave model)
        {
            return _repository.Update(model);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("api/Leave/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }

        [Route("api/Leave/GetPendingLeave")]
        [HttpGet]
        public async Task<List<LeaveViewModel>> GetPendingLeave()
        {
            var data = await _iCustomerService.GetPendingLeaves();
            return data.ToList();
        }

        [Route("api/Leave/ApproveLeaves/{id}")]
        [HttpGet]
        public bool ApproveLeaves(Guid id)
        {
            var data = _repository.GetById(id);
            if(data != null)
            {
                data.LeaveStatus = Enums.LeaveStatus.Approved;
                _repository.Update(data);
                return true;
            }
            else
            {
                return false;
            }
        }

        [Route("api/Leave/RejectedLeaves/{id}")]
        [HttpGet]
        public bool RejectedLeaves(Guid id)
        {
            var data = _repository.GetById(id);
            if (data != null)
            {
                data.LeaveStatus = Enums.LeaveStatus.Rejected;
                _repository.Update(data);
                return true;
            }
            else
            {
                return false;
            }
        }

        //[Route("api/Leave/Filter/{name}/{fromdate}")]
        [Route("api/Leave/Filter")]
        [HttpGet]
        public async Task<List<LeaveViewModel>> GetFilter(string name, DateTime fromdate, DateTime todate, Int32 leavetype, Int32 leavestatus)
        {
            try
            {
                var data = await _iCustomerService.GetFilters(name, fromdate, todate, leavetype, leavestatus);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
