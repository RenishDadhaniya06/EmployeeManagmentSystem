
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
    /// TypesController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TypesController : ApiController
    {
        #region Properties
        private IRepository<TemplatesType> _repository;
        #endregion

        public TypesController(IRepository<TemplatesType> repository)
        {
            _repository = repository;
        }
        // GET: TemplateType
        [Route("api/TemplateType/GetTemplateTypes")]
        public IEnumerable<TemplatesType> GetTemplateTypes()
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

        [Route("api/TemplateType/Get/{id}")]
        [HttpGet]
        public TemplatesType Get(Guid id)
        {
            try
            {
                TemplatesType data = _repository.GetById(id);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/TemplateType/Post")]
        [HttpPost]
        public TemplatesType Post(TemplatesType model)
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

        [Route("api/TemplateType/Put")]
        [HttpPut]
        public TemplatesType Put(TemplatesType model)
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

        [Route("api/TemplateType/Delete/{id}")]
        [HttpDelete]
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