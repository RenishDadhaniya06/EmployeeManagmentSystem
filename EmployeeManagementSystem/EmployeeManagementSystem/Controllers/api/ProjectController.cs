
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
    /// ProjectController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProjectController : ApiController
    {
        #region Properties
        private IRepository<Projects> _repository;
        #endregion

        #region Constructor
        public ProjectController(IRepository<Projects> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get All Method
        [Route("api/Project/GetProjects")]
        public IEnumerable<Projects> GetCities()
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
        #endregion

        #region GetbyId Method
        [Route("api/Project/Get/{id}")]
        public Projects Get(Guid id)
        {
            try
            {
                var data = _repository.GetById(id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Post Method
        [Route("api/Project/Post")]
        public Projects Post(Projects model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                var data = _repository.Insert(model);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Put Method
        [Route("api/Project/Put")]
        public Projects Put(Projects model)
        {
            try
            {
                return _repository.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Delete Method
        [Route("api/Project/Delete/{id}")]
        public bool Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
