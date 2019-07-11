using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http;


namespace EmployeeManagementSystem.Controllers.api
{
    public class StateController : ApiController
    {
        private IRepository<States> _repository;

        public StateController(IRepository<States> repository)
        {
            _repository = repository;
        }

        [Route("api/State/GetStates")]
        public IEnumerable<States> GetStates()
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

        [Route("api/State/Get/{id}")]
        public States Get(Guid id)
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

        [Route("api/State/Post")]
        public States Post(States model)
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

        [Route("api/State/Put")]
        public States Put(States model)
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

        [Route("api/State/Delete/{id}")]
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