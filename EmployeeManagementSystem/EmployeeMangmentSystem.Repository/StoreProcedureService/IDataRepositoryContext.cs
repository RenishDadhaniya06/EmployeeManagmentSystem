﻿
namespace EmployeeMangmentSystem.Repository.StoreProcedureService
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    #endregion
    public interface IDataRepositoryContext
    {

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetAll();

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

        /// <summary>
        /// Gets the leaves.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<List<Leave>> GetLeavesByEmployee(Guid id);

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <returns></returns>
        Task<List<Notifications>> GetMessages();

        /// <summary>
        /// Gets the hr.
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetHR();

        /// <summary>
        /// Gets the leave template.
        /// </summary>
        /// <returns></returns>
        Task<Templates> GetLeaveTemplate();

        /// <summary>
        /// Gets the pending leaves.
        /// </summary>
        /// <returns></returns>
        Task<List<LeaveViewModel>> GetPendingLeaves();

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fromdate">The fromdate.</param>
        /// <param name="todate">The todate.</param>
        /// <param name="leavetype">The leavetype.</param>
        /// <param name="leavestatus">The leavestatus.</param>
        /// <returns></returns>
        Task<List<LeaveViewModel>> GetFilters(string name,DateTime fromdate,DateTime todate,Int32 leavetype,Int32 leavestatus);

        /// <summary>
        /// Gets the openings.
        /// </summary>
        /// <returns></returns>
        Task<List<OpeningsViewModel>> GetOpenings();

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<Employee> GetEmployee(string email);

        /// <summary>
        /// Gets the candidates.
        /// </summary>
        /// <returns></returns>
        Task<List<DisplayCandidateViewModel>> GetCandidates();

        /// <summary>
        /// Deletes the candidate skill.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool DeleteCandidateSkill(Guid id);

        /// <summary>
        /// Deletes the candidate technology.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool DeleteCandidateTechnology(Guid id);

        /// <summary>
        /// Gets the filter candidate.
        /// </summary>
        /// <param name="skills">The skills.</param>
        /// <param name="technologies">The technologies.</param>
        /// <returns></returns>
        Task<List<DisplayCandidateViewModel>> GetFilterCandidate(string skills,string technologies);

        /// <summary>
        /// Gets the interviewers.
        /// </summary>
        /// <returns></returns>
        Task<List<DisplayInterviewerModel>> GetInterviewers();

        /// <summary>
        /// Gets the interviews list.
        /// </summary>
        /// <returns></returns>
        Task<List<DisplayInterviewModel>> GetInterviewsList();

        /// <summary>
        /// Gets the candidate search detail.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<List<Candidates>> GetCandidateSearchDetail(string name);

        Task<EmployeeUserViewModel> GetEmployeeUserViewModel(Guid id);

        Task<List<ProjectTeamViewModel>> GetProjects();

        Task<List<EmployeeUserViewModel>> GetAvailableResources(Guid id);
    }
}
