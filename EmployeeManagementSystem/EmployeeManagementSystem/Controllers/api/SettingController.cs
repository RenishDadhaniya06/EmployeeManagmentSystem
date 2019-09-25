
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using System;
    using System.Linq;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// SettingController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SettingController : ApiController
    {
        #region Properties
        private IRepository<SettingView> _repository;
        #endregion

        public SettingController(IRepository<SettingView> repository)
        {
            _repository = repository;
        }

        [Route("api/Setting/Post")]
        public SettingView Post(SettingView model)
        {
            try
            {
                var data = _repository.Update(model);
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