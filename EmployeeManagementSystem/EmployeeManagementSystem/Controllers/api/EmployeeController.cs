using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers.api
{
    public class EmployeeController : ApiController
    {
        private IRepository<Employee> _repository;

        private ICustomerService _iCustomerService;

        public EmployeeController(IRepository<Employee> repository,ICustomerService customerService)
        {
            _repository = repository;
            _iCustomerService = customerService;
        }

        [Route("api/Employee/GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                var data = _repository.GetAll();
                return data;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Route("api/Employee/Get/{id}")]
        [HttpGet]
        public Employee Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Employee/Post")]
        [HttpPost]
        public Employee Post(Employee model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Employee/Put")]
        [HttpPut]
        public Employee Put(Employee model)
        {
            return _repository.Update(model);
        }

        [Route("api/Employee/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }

        [Route("api/Employee/GetEmployee")]
        [HttpGet]
        public async Task<Employee> GetEmployee(string email)
        {
            var data = await _iCustomerService.GetEmployee(email);
            return data;
        }
    }
}
