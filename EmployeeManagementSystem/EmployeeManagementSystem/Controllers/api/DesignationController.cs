
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
    /// DesignationController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DesignationController : ApiController
    {
        #region Properties
        private IRepository<Designation> _repository;
        #endregion

        public DesignationController(IRepository<Designation> repository)
        {
            _repository = repository;
        }

        [Route("api/Designation/GetDesignations")]
        public IEnumerable<Designation> GetDesignations()
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
