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
    public class InterviewerController : ApiController
    {
        private IRepository<Interviewers> _repository;

        private ICustomerService _customerService;

        public InterviewerController(IRepository<Interviewers> repository, ICustomerService customerService)
        {
            _repository = repository;
            _customerService = customerService;
        }

        [Route("api/Interviewer/GetInterviewers")]
        public IEnumerable<Interviewers> GetInterviewers()
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

        [Route("api/Interviewer/GetInterviewerList")]
        public async Task<List<DisplayInterviewerModel>> GetInterviewerList()
        {
            try
            {
                var data = await _customerService.GetInterviewers();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Interviewer/Get/{id}")]
        public Interviewers Get(Guid id)
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

        [Route("api/Interviewer/Post")]
        public Interviewers Post(Interviewers model)
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

        [Route("api/Interviewer/Put")]
        public Interviewers Put(Interviewers model)
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

        [Route("api/Interviewer/Delete/{id}")]
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
