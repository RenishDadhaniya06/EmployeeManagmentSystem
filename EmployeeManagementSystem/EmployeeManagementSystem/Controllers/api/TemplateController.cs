﻿using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http;


namespace EmployeeManagementSystem.Controllers.api
{
    public class TemplateController : ApiController
    {
        private IRepository<Templates> _repository;

        public TemplateController(IRepository<Templates> repository)
        {
            _repository = repository;
        }
        
        [Route("api/Template/GetTemplates")]
        public IEnumerable<Templates> GetTemplates()
        {
            var data = _repository.GetAll();
            return data;
        }

        [Route("api/Templates/Get/{id}")]
        [HttpGet]
        public Templates Get(Guid id)
        {
            var data = _repository.GetById(id);
            return data;
        }

        [Route("api/Templates/Post")]
        [HttpPost]
        public Templates Post(Templates model)
        {
            model.Id = Guid.NewGuid();
            var data = _repository.Insert(model);
            return data;
        }

        [Route("api/Templates/Put")]
        [HttpPut]
        public Templates Put(Templates model)
        {
            return _repository.Update(model);
        }

        [Route("api/Templates/Delete/{id}")]
        public bool Delete(Guid id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}