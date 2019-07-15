using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMangmentSystem.Repository.StoreProcedureService
{
   public interface IDataRepositoryContext
    {

        IEnumerable<Customer> GetAll();

        Task<List<RolePermission>> GetRolesById(Guid roleId);

        Task<List<RolesViewModel>> GetRoles();
    }
}
