////-----------------------------------------------------------------------
//// <copyright file="CustomerService.cs" company="Albiorix">
////    Albiorix Technologies Pvt. LTD
//// </copyright>
////-----------------------------------------------------------------------

namespace EmployeeMangmentSystem.Services.Services.Classes
{
    using System;
    #region using
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.StoreProcedureService;
    
    #endregion

    /// <summary>Customer Service</summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>The database context</summary>
        private readonly IDataRepositoryContext dbContext;

        /// <summary>Initializes a new instance of the <see cref="CustomerService"/> class.</summary>
        /// <param name="iCustomerRepository">The i customer repository.</param>
        public CustomerService(IDataRepositoryContext iCustomerRepository)
        {
            dbContext = iCustomerRepository;
        }

        /// <summary>Gets the customers.</summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return dbContext.GetAll();
        }

        public async Task<List<RolePermission>> GetRolesById(Guid roleId)
        {
            return await dbContext.GetRolesById(roleId);
        }

        public async Task<List<RolesViewModel>> GetRoles()
        {
            return await dbContext.GetRoles();
        }

        public async Task<bool> DeletebyRoleId(Guid id)
        {
            return await dbContext.DeletebyRoleId(id);
        }
    }
}
