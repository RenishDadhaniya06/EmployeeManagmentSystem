
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// TechnologyController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TechnologyController : ApiController
    {
        #region Properties
        private IRepository<Technologies> _repository;
        #endregion

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
