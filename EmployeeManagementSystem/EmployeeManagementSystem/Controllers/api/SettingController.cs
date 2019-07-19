using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace EmployeeManagementSystem.Controllers.api
{
    public class SettingController : ApiController
    {
        private IRepository<SettingView> _repository;

        public SettingController(IRepository<SettingView> repository)
        {
            _repository = repository;
        }

        [Route("api/Setting/Post")]
        public SettingView Post(SettingView model)
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

        [Route("api/Setting/GetSettings")]
        public SettingView GetSettings()
        {
            try
            {
                var data = _repository.GetAll().ToList();
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}