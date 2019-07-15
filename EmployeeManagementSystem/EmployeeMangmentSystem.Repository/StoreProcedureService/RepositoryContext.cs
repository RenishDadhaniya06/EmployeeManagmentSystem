#region usings
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.StoreProcedureService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
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

        public async Task<List<RolePermission>> GetRolesById(Guid roleId)
        {
            var data = await Database.SqlQuery<RolePermission>(@"exec [dbo].[RolePermissionById]",roleId).ToListAsync();
            return data;
        }

        public async Task<List<RolesViewModel>> GetRoles()
        {
            var data = await Database.SqlQuery<RolesViewModel>(@"exec [dbo].[GetRoles]").ToListAsync();
            return data;
        }
    }
}
