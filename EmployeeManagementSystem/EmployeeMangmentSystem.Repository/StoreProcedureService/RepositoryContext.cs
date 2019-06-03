#region usings
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.StoreProcedureService;
using System.Collections.Generic;
using System.Data.Entity;
#endregion

namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
    /// <summary>
    /// Repository Context
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="EmployeeMangmentSystem.Repository.StoreProcedureService.IDataRepositoryContext" />
    public partial class RepositoryContext : DbContext, IDataRepositoryContext
    {
        /// <summary>Gets all.</summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAll()
        {
            return null;
        }
    }
}
