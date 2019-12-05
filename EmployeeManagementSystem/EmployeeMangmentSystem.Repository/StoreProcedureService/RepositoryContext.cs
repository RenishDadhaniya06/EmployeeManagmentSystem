

namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
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

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Notifications>> GetMessages()
        {
            return await Database.SqlQuery<Notifications>(@"exec [dbo].[GetNotification]").ToListAsync();
        }

        /// <summary>
        /// Gets the hr.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetHR()
        {
            var data = await Database.SqlQuery<string>(@"exec [dbo].[GetHRRole]").ToListAsync();
            //return await Database.SqlQuery<string>(@"exec [dbo].[GetHRRole]").SingleOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// Gets the leave template.
        /// </summary>
        /// <returns></returns>
        public async Task<Templates> GetLeaveTemplate()
        {
            return await Database.SqlQuery<Templates>(@"exec [dbo].[GetLeaveTemplate]").SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets the pending leaves.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveViewModel>> GetPendingLeaves()
        {
            //return await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetPendingLeaves]").ToListAsync();
            var data = await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetPendingLeaves]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fromdate">The fromdate.</param>
        /// <param name="todate">The todate.</param>
        /// <param name="leavetype">The leavetype.</param>
        /// <param name="leavestatus">The leavestatus.</param>
        /// <returns></returns>
        public async Task<List<LeaveViewModel>> GetFilters(string name, DateTime fromdate,DateTime todate,Int32 leavetype,Int32 leavestatus)
        {
            var datefrom = fromdate.ToString("yyyy-dd-MM");
            var dateto = todate.ToString("yyyy-dd-MM");
            var data = await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetFilterData] @p0,@p1,@p2,@p3,@p4", name, datefrom,dateto,leavetype,leavestatus).ToListAsync();
            //return await Database.SqlQuery<LeaveViewModel>(@"exec [dbo].[GetFilterData] @p0 @p1", name,fromdate).ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the openings.
        /// </summary>
        /// <returns></returns>
        public async Task<List<OpeningsViewModel>> GetOpenings()
        {
            var data = await Database.SqlQuery<OpeningsViewModel>(@"exec [dbo].[GetOpenings]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee(string email)
        {
            var data = await Database.SqlQuery<Employee>(@"exec [dbo].[GetEmployee] @p0",email).SingleOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// Gets the candidates.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayCandidateViewModel>> GetCandidates()
        {
            var data = await Database.SqlQuery<DisplayCandidateViewModel>(@"exec [dbo].[GetCandidateList]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Deletes the candidate skill.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteCandidateSkill(Guid id)
        {
            //var data = await Database.SqlQuery<bool>(@"exec [dbo].[DeleteCandidateSkill]").ToListAsync();
            Database.ExecuteSqlCommand(@"exec [dbo].[DeleteCandidateSkill] @p0",id);
            return true;
        }

        /// <summary>
        /// Deletes the candidate technology.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteCandidateTechnology(Guid id)
        {
            Database.ExecuteSqlCommand(@"exec [dbo].[DeleteCandidateTechnology] @p0", id);
            return true;
        }
        public async Task<List<Employee>> GetEmployeeByRole(string id)
        {
            var data = await Database.SqlQuery<Employee>(@"exec [dbo].[GetEmployeeByRole] @p0", id).ToListAsync();
            return data;
        }
        /// <summary>
        /// Gets the filter candidate.
        /// </summary>
        /// <param name="skills">The skills.</param>
        /// <param name="technologies">The technologies.</param>
        /// <returns></returns>
        public async Task<List<DisplayCandidateViewModel>> GetFilterCandidate(string skills,string technologies)
        {
            string skill = skills.ToString();
            string technology = technologies.ToString();
            var data = await Database.SqlQuery<DisplayCandidateViewModel>(@"exec [dbo].[CandidateFilter] @p0,@p1", skill,technology).ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the interviewers.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayInterviewerModel>> GetInterviewers()
        {
            var data = await Database.SqlQuery<DisplayInterviewerModel>(@"exec [dbo].[GetInterviewers]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the interviews list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayInterviewModel>> GetInterviewsList()
        {
            var data = await Database.SqlQuery<DisplayInterviewModel>(@"exec [dbo].[GetInterviewsList]").ToListAsync();
            return data;
        }

        /// <summary>
        /// Gets the candidate search detail.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<List<Candidates>> GetCandidateSearchDetail(string name)
        {
            var data = await Database.SqlQuery<Candidates>(@"exec [dbo].[AutoCompleteCandidate] @p0", name).ToListAsync();
            return data;
        }

        public async Task<EmployeeUserViewModel> GetEmployeeUserViewModel(Guid id)
        {
            var data = await Database.SqlQuery<EmployeeUserViewModel>(@"exec [dbo].[GetEmployeeUserViewModel] @p0", id).SingleOrDefaultAsync();
            return data;
        }

        public async Task<List<ProjectTeamViewModel>> GetProjects()
        {
            var data = await Database.SqlQuery<ProjectTeamViewModel>(@"exec [dbo].[GetProjects]").ToListAsync();
            return data;
        }
        public async Task<List<ProjectTeamViewModel>> GetProjectsByUserId(string id)
        {
            return await Database.SqlQuery<ProjectTeamViewModel>(@"exec [dbo].[GetProjectsByUserId] @p0", id).ToListAsync();
        }


        public async Task<List<TeamViewModel>> TeamByProjectIdGet(Guid id)
        {
            var data = await Database.SqlQuery<TeamViewModel>(@"exec [dbo].[TeamByProjectIdGet] @p0", id).ToListAsync();
            return data;
        }

        public async Task<List<EmployeeUserViewModel>> GetAvailableResources(Guid id, bool workingid)
        {
            var data = await Database.SqlQuery<EmployeeUserViewModel>(@"exec [dbo].[GetAvailableResourcebySkills] @p0,@p1",id,workingid).ToListAsync();
            return data;
        }

        public async Task<List<ProjectViewModel>> GetProjectsbyUserId(Guid id)
        {
            var data = await Database.SqlQuery<ProjectViewModel>(@"exec [dbo].[GetProjectTeambyUserId] @p0", id).ToListAsync();
            return data;
        }

        public async Task<DashboardCounts> GetDashboardCounts()
        {
            var data = await Database.SqlQuery<DashboardCounts>(@"exec [dbo].[DashboardCounts]").SingleOrDefaultAsync();
            return data;
        }
    }
}
