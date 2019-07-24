using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers.api
{
    public class DepartmentController : ApiController
    {
        private IRepository<Departments> _repository;

        public DepartmentController(IRepository<Departments> repository)
        {
            _repository = repository;
        }
        [Route("api/Department/GetDepartments")]
        public IEnumerable<Departments> GetDesignations()
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

        [Route("api/Department/Get/{id}")]
        [HttpGet]
        public Departments Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Department/Post")]
        [HttpPost]
        public Departments Post(Departments model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Department/Put")]
        [HttpPut]
        public Departments Put(Departments model)
        {
            return _repository.Update(model);
        }

        [Route("api/Department/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}
