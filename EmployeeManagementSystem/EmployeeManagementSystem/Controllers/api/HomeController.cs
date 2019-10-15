

namespace Web.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System.Collections.Generic;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// HomeController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HomeController : ApiController
    {
        #region Properties
        private ICustomerService _iCustomerService;
        private IRepository<Customer> _repository;
        #endregion

        public HomeController(ICustomerService iCustomerService, IRepository<Customer> repository)
        {
            _iCustomerService = iCustomerService;
            _repository = repository;
        }

        [Route("api/Home/GetCustomers")]
        public IEnumerable<Customer> GetCustomers()
        {
            var data = _repository.GetAll();
            _repository.FindBy(_ => _.CustomerName == "Renish");
            return data;
        }

        public Customer Get(int id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }

        [Route("api/Home/Post")]
        [HttpPost]
        public Customer Post(Customer customer)
        {
            var data2 = _repository.Insert(customer);
            return data2;
        }

        [Route("api/Home/Put")]
        [HttpPut]
        public Customer Put(Customer customer)
        {
            return _repository.Update(customer);
        }

        [Route("api/Home/Delete/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            _repository.Delete(id);
            return true;
        }
    }
}
