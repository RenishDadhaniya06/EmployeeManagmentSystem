
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    #endregion
    public class NotificationController : ApiController
    {
        #region Properties
        private IRepository<Notifications> _repository;
        private ICustomerService _iCustomerService;
        #endregion

        public NotificationController(IRepository<Notifications> repository,ICustomerService customerService)
        {
            _repository = repository;
            _iCustomerService = customerService;
        }

        [Route("api/Notification/GetNotifications")]
        public IEnumerable<Notifications> GetNotifications()
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

        [Route("api/Notification/Get/{id}")]
        [HttpGet]
        public Notifications Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Notification/Post")]
        [HttpPost]
        public Notifications Post(Notifications model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Notification/Put")]
        [HttpPut]
        public Notifications Put(Notifications model)
        {
            return _repository.Update(model);
        }

        [Route("api/Notification/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }

        //[Route("api/Notification/GetMessage")]
        //public async Task<Notifications> GetMessages()
        //{
        //    try
        //    {
        //        Notifications temp = new Notifications();
        //        var data = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetNotifications");
        //        foreach (var item in data)
        //        {
        //            if (DateTime.Now.Date == item.From && DateTime.Now.Date <= item.To)
        //            {
        //                temp = item;
        //            }
        //        }
        //        return temp;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        [Route("api/Notification/GetMessage")]
        public async Task<List<Notifications>> GetMessages()
        {
            try
            {
                var data = await _iCustomerService.GetMessages();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}