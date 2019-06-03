using EmployeeMangmentSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.StoreProcedureService
{
   public interface IDataRepositoryContext
    {

        IEnumerable<Customer> GetAll();
    }
}
