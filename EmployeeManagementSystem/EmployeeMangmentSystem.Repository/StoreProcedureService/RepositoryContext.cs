﻿#region usings
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.StoreProcedureService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        /// <summary>
        /// Gets the roles by identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public async Task<List<RolePermission>> GetRolesById(Guid roleId)
        {
            var data = await Database.SqlQuery<RolePermission>(@"exec [dbo].[RolePermissionById] @p0", roleId).ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RolesViewModel>> GetRoles()
        {
            var data = await Database.SqlQuery<RolesViewModel>(@"exec [dbo].[GetRoles]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Deletebies the role identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeletebyRoleId(Guid id)
        {
            await Database.SqlQuery<bool>(@"exec [dbo].[DeleteRolePermissionsbyRoleid] @p0", id).ToListAsync();
            return true;
        }

        /// <summary>
        /// Gets the leaves.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<List<Leave>> GetLeavesByEmployee(Guid id)
        {
            return await Database.SqlQuery<Leave>(@"exec [dbo].[GetLeavesByEmployeeId] @p0", id).ToListAsync();
        }

        public async Task<List<Notifications>> GetMessages()
        {
            return await Database.SqlQuery<Notifications>(@"exec [dbo].[GetNotification]").ToListAsync();
        }

        public async Task<List<string>> GetHR()
        {
            var data = await Database.SqlQuery<string>(@"exec [dbo].[GetHRRole]").ToListAsync();
            //return await Database.SqlQuery<string>(@"exec [dbo].[GetHRRole]").SingleOrDefaultAsync();
            return data;
        }

        public async Task<Templates> GetLeaveTemplate()
        {
            return await Database.SqlQuery<Templates>(@"exec [dbo].[GetLeaveTemplate]").SingleOrDefaultAsync();
        }

        public async Task<List<LeaveViewModel>> GetPendingLeaves()
        {
            return await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetPendingLeaves]").ToListAsync();
        }

        public async Task<List<LeaveViewModel>> GetFilters(string name, DateTime fromdate,DateTime todate,Int32 leavetype,Int32 leavestatus)
        {
            var datefrom = fromdate.ToString("yyyy-dd-MM");
            var dateto = todate.ToString("yyyy-dd-MM");
            var data = await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetFilterData] @p0,@p1,@p2,@p3,@p4", name, datefrom,dateto,leavetype,leavestatus).ToListAsync();
            //return await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetFilterData] @p0 @p1", name,fromdate).ToListAsync();
            return data;
        }
    }
}
