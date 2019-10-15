
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
    /// CountryController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CountryController : ApiController
    {
        #region Properties
        private IRepository<Countries> _repository;
        #endregion

        public CountryController(IRepository<Countries> repository)
        {
            _repository = repository;
        }
       
        [Route("api/Country/GetCountries")]
        public IEnumerable<Countries> GetCountries()
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

        [Route("api/Country/Get/{id}")]
        public Countries Get(Guid id)
        {
            try
            {
                Countries data = _repository.GetById(id);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Country/Post")]
        public Countries Post(Countries model)
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

        [Route("api/Country/Put")]
        public Countries Put(Countries model)
        {
            try
            {
                return _repository.Update(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/Country/Delete/{id}")]
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
    }
}