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
    public class DesignationController : ApiController
    {
        private IRepository<Designation> _repository;

        public DesignationController(IRepository<Designation> repository)
        {
            _repository = repository;
        }

        [Route("api/Designation/GetDesignations")]
        public IEnumerable<Designation> GetDesignations()
        {
            var data = _repository.GetAll();
            return data;
        }

        [Route("api/Designation/Get/{id}")]
        [HttpGet]
        public Designation Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Designation/Post")]
        [HttpPost]
        public Designation Post(Designation model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Designation/Put")]
        [HttpPut]
        public Designation Put(Designation model)
        {
            return _repository.Update(model);
        }

        [Route("api/Designation/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}
