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
    public class TechnologyController : ApiController
    {
        private IRepository<Technologies> _repository;

        public TechnologyController(IRepository<Technologies> repository)
        {
            _repository = repository;
        }
        [Route("api/Technology/GetTechnologies")]
        public IEnumerable<Technologies> GetDesignations()
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

        [Route("api/Technology/Get/{id}")]
        [HttpGet]
        public Technologies Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Technology/Post")]
        [HttpPost]
        public Technologies Post(Technologies model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Technology/Put")]
        [HttpPut]
        public Technologies Put(Technologies model)
        {
            return _repository.Update(model);
        }

        [Route("api/Technology/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}
