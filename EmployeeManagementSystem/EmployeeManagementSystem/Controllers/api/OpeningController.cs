

namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// OpeningController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class OpeningController : ApiController
    {
        #region Properties
        private IRepository<Openings> _repository;
        private ICustomerService _customerService;
        #endregion

        public OpeningController(IRepository<Openings> repository,ICustomerService customerService)
        {
            _repository = repository;
            _customerService = customerService;
        }

        [Route("api/Openings/GetOpenings")]
        public async Task<List<OpeningsViewModel>> GetOpenings()
        {
            try
            {
                //var data = _repository.GetAll();
                var data = await _customerService.GetOpenings();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Openings/Get/{id}")]
        public Openings Get(Guid id)
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

        [Route("api/Openings/Post")]
        public Openings Post(Openings model)
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

        [Route("api/Openings/Put")]
        public Openings Put(Openings model)
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

        [Route("api/Openings/Delete/{id}")]
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
