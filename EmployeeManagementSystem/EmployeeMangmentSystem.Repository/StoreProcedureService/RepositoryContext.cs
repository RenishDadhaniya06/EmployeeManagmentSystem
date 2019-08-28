#region usings
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

        public async Task<List<OpeningsViewModel>> GetOpenings()
        {
            var data = await Database.SqlQuery<OpeningsViewModel>(@"exec [dbo].[GetOpenings]").ToListAsync();
            return data;
        }

        public async Task<Employee> GetEmployee(string email)
        {
            var data = await Database.SqlQuery<Employee>(@"exec [dbo].[GetEmployee] @p0",email).SingleOrDefaultAsync();
            return data;
        }

        public async Task<List<DisplayCandidateViewModel>> GetCandidates()
        {
            var data = await Database.SqlQuery<DisplayCandidateViewModel>(@"exec [dbo].[GetCandidateList]").ToListAsync();
            return data;
        }

        public bool DeleteCandidateSkill(Guid id)
        {
            //var data = await Database.SqlQuery<bool>(@"exec [dbo].[DeleteCandidateSkill]").ToListAsync();
            Database.ExecuteSqlCommand(@"exec [dbo].[DeleteCandidateSkill] @p0",id);
            return true;
        }

        public bool DeleteCandidateTechnology(Guid id)
        {
            Database.ExecuteSqlCommand(@"exec [dbo].[DeleteCandidateTechnology] @p0", id);
            return true;
        }

        public async Task<List<DisplayCandidateViewModel>> GetFilterCandidate(string skills,string technologies)
        {
            string skill = skills.ToString();
            string technology = technologies.ToString();
            var data = await Database.SqlQuery<DisplayCandidateViewModel>(@"exec [dbo].[CandidateFilter] @p0,@p1", skill,technology).ToListAsync();
            return data;
        }

        public async Task<List<DisplayInterviewerModel>> GetInterviewers()
        {
            var data = await Database.SqlQuery<DisplayInterviewerModel>(@"exec [dbo].[GetInterviewers]").ToListAsync();
            return data;
        }

        public async Task<List<DisplayInterviewModel>> GetInterviewsList()
        {
            var data = await Database.SqlQuery<DisplayInterviewModel>(@"exec [dbo].[GetInterviewsList]").ToListAsync();
            return data;
        }

        public async Task<List<Candidates>> GetCandidateSearchDetail(string name)
        {
            var data = await Database.SqlQuery<Candidates>(@"exec [dbo].[AutoCompleteCandidate] @p0", name).ToListAsync();
            return data;
        }
    }
}
