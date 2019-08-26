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

        Task<List<Notifications>> GetMessages();

        Task<List<string>> GetHR();

        Task<Templates> GetLeaveTemplate();

        Task<List<LeaveViewModel>> GetPendingLeaves();

        Task<List<LeaveViewModel>> GetFilters(string name,DateTime fromdate,DateTime todate,Int32 leavetype,Int32 leavestatus);

        Task<List<OpeningsViewModel>> GetOpenings();

        Task<Employee> GetEmployee(string email);

        Task<List<DisplayCandidateViewModel>> GetCandidates();

        bool DeleteCandidateSkill(Guid id);

        bool DeleteCandidateTechnology(Guid id);

        Task<List<DisplayCandidateViewModel>> GetFilterCandidate(string skills, string technologies);

        Task<List<DisplayInterviewModel>> GetInterviews();
    }
}
