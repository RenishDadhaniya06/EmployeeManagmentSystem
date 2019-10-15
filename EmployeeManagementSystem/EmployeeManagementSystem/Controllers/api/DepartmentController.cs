
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
    /// DepartmentController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DepartmentController : ApiController
    {
        #region Properties
        private IRepository<Departments> _repository;
        #endregion

        public DepartmentController(IRepository<Departments> repository)
        {
            _repository = repository;
        }
        [Route("api/Department/GetDepartments")]
        public IEnumerable<Departments> GetDeparments()
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
