using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers.api
{
    public class InterviewController : ApiController
    {
        private IRepository<Interviews> _repository;

        private ICustomerService _customerService;

        public InterviewController(IRepository<Interviews> repository,ICustomerService customerService)
        {
            _repository = repository;
            _customerService = customerService;
        }

        [Route("api/Interview/GetInterviews")]
        public IEnumerable<Interviews> GetInterviews()
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

        [Route("api/Interview/GetInterviewsList")]
        public async Task<List<DisplayInterviewModel>> GetInterviewsList()
        {
            try
            {
                var data = await _customerService.GetInterviewsList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Interview/Get/{id}")]
        public Interviews Get(Guid id)
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

        [Route("api/Interview/Post")]
        public Interviews Post(Interviews model)
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

        [Route("api/Interview/Put")]
        public Interviews Put(Interviews model)
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

        [Route("api/Interview/Delete/{id}")]
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

        [Route("api/Interview/GetCandidateDetail")]
        [HttpGet]
        public async Task<List<Candidates>> GetCandidateDetail(string name)
        {
            try
            {
                var data = await _customerService.GetCandidateSearchDetail(name);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}