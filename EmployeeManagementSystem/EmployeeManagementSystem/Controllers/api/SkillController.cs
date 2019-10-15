
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
    /// SkillController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SkillController : ApiController
    {
        #region Properties
        private IRepository<Skills> _repository;
        #endregion

        public SkillController(IRepository<Skills> repository)
        {
            _repository = repository;
        }
        [Route("api/Skill/GetSkills")]
        public IEnumerable<Skills> GetDesignations()
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

        [Route("api/Skill/Get/{id}")]
        [HttpGet]
        public Skills Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Skill/Post")]
        [HttpPost]
        public Skills Post(Skills model)
        {
            model.Id = Guid.NewGuid();
            var data2 = _repository.Insert(model);
            return data2;
        }

        [Route("api/Skill/Put")]
        [HttpPut]
        public Skills Put(Skills model)
        {
            return _repository.Update(model);
        }

        [Route("api/Skill/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}
