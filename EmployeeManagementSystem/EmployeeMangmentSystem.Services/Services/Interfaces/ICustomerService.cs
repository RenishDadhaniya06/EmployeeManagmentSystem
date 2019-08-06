using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeMangmentSystem.Services.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Gets the roles by identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        Task<List<RolePermission>> GetRolesById(Guid roleId);

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        Task<List<RolesViewModel>> GetRoles();

        /// <summary>
        /// Deletebies the role identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeletebyRoleId(Guid id);

        #region Leaves
        /// <summary>
        /// Gets the leaves by employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<List<Leave>> GetLeavesByEmployee(Guid id);
        #endregion

        Task<List<Notifications>> GetMessages();

        Task<List<string>> GetHR();

        Task<Templates> GetLeaveTemplate();

        Task<List<LeaveViewModel>> GetPendingLeaves();
    }
}
