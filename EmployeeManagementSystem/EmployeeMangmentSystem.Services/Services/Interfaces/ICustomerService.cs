using EmployeeMangmentSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Services.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
    }
}
