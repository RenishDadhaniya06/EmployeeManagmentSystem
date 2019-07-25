using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

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
    }
}
