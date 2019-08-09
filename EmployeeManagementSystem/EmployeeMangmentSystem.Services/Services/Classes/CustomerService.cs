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

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return dbContext.GetAll();
        }

        /// <summary>
        /// Gets the roles by identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public async Task<List<RolePermission>> GetRolesById(Guid roleId)
        {
            return await dbContext.GetRolesById(roleId);
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RolesViewModel>> GetRoles()
        {
            return await dbContext.GetRoles();
        }

        /// <summary>
        /// Deletebies the role identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeletebyRoleId(Guid id)
        {
            return await dbContext.DeletebyRoleId(id);
        }

        /// <summary>
        /// Gets the leaves by employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<List<Leave>> GetLeavesByEmployee(Guid id)
        {
            return await dbContext.GetLeavesByEmployee(id);
        }

        public async Task<List<Notifications>> GetMessages()
        {
            return await dbContext.GetMessages();
        }

        public async Task<List<string>> GetHR()
        {
            return await dbContext.GetHR();
        }

        public async Task<Templates> GetLeaveTemplate()
        {
            return await dbContext.GetLeaveTemplate();
        }

        public async Task<List<LeaveViewModel>> GetPendingLeaves()
        {
            return await dbContext.GetPendingLeaves();
        }

        public async Task<List<LeaveViewModel>> GetFilters(string name, DateTime fromdate,DateTime todate,Int32 leavetype,Int32 leavestatus)
        {
            var data = await dbContext.GetFilters(name, fromdate,todate,leavetype,leavestatus);
            //return await dbContext.GetFilters(name,fromdate);
            return data;
        }
    }
}
