using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeMangmentSystem.Services.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();

         Task<List<RolePermission>>  GetRolesById(Guid roleId);

        Task<List<RolesViewModel>> GetRoles();

        Task<bool> DeletebyRoleId(Guid id);
    }
}
