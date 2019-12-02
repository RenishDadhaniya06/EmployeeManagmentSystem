////-----------------------------------------------------------------------
//// <copyright file="CustomerService.cs" company="Albiorix">
////    Albiorix Technologies Pvt. LTD
//// </copyright>
////-----------------------------------------------------------------------

namespace EmployeeMangmentSystem.Services.Services.Classes
{
    
    #region using
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.StoreProcedureService;
    using System;
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

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Notifications>> GetMessages()
        {
            return await dbContext.GetMessages();
        }

        /// <summary>
        /// Gets the hr.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetHR()
        {
            return await dbContext.GetHR();
        }

        /// <summary>
        /// Gets the leave template.
        /// </summary>
        /// <returns></returns>
        public async Task<Templates> GetLeaveTemplate()
        {
            return await dbContext.GetLeaveTemplate();
        }

        /// <summary>
        /// Gets the pending leaves.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveViewModel>> GetPendingLeaves()
        {
            return await dbContext.GetPendingLeaves();
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
            var data = await dbContext.GetFilters(name, fromdate,todate,leavetype,leavestatus);
            //return await dbContext.GetFilters(name,fromdate);
            return data;
        }

        /// <summary>
        /// Gets the openings.
        /// </summary>
        /// <returns></returns>
        public async Task<List<OpeningsViewModel>> GetOpenings()
        {
            var data = await dbContext.GetOpenings();
            return data;
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee(string email)
        {
            var data = await dbContext.GetEmployee(email);
            return data;
        }

        /// <summary>
        /// Gets the candidates.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayCandidateViewModel>> GetCandidates()
        {
            var data = await dbContext.GetCandidates();
            return data;
        }

        /// <summary>
        /// Deletes the candidate skill.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteCandidateSkill(Guid id)
        {
            return  dbContext.DeleteCandidateSkill(id);
        }

        /// <summary>
        /// Deletes the candidate technology.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteCandidateTechnology(Guid id)
        {
            return dbContext.DeleteCandidateTechnology(id);
        }

        /// <summary>
        /// Gets the filter candidate.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="technologies">The technologies.</param>
        /// <returns></returns>
        public async Task<List<DisplayCandidateViewModel>> GetFilterCandidate(string skill,string technologies)
        {
            var data = await dbContext.GetFilterCandidate(skill,technologies);
            return data;
        }

        /// <summary>
        /// Gets the interviewers.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayInterviewerModel>> GetInterviewers()
        {
            return await dbContext.GetInterviewers();
        }

        /// <summary>
        /// Gets the interviews list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayInterviewModel>> GetInterviewsList()
        {
            return await dbContext.GetInterviewsList();
        }

        /// <summary>
        /// Gets the candidate search detail.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<List<Candidates>> GetCandidateSearchDetail(string name)
        {
            return await dbContext.GetCandidateSearchDetail(name);
        }

        public async Task<EmployeeUserViewModel> GetEmployeeUserViewModel(Guid id)
        {
            return await dbContext.GetEmployeeUserViewModel(id);
        }

        public async Task<List<ProjectTeamViewModel>> GetProjects()
        {
            return await dbContext.GetProjects();
        }

        public async Task<List<EmployeeUserViewModel>> GetAvailableResources(Guid id)
        {
            return await dbContext.GetAvailableResources(id);
        }

        public async Task<List<TeamViewModel>> TeamByProjectIdGet(Guid id)
        {
            return await dbContext.TeamByProjectIdGet(id);
        }
    }
}
