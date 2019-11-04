
namespace EmployeeManagementSystem.Helper
{
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Repository.Classes;
    using System;
    using System.Linq;
    using System.Web;
    #endregion


    /// <summary>
    /// CommonHelper
    /// </summary>
    public static class CommonHelper
    {
        #region Properties
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        private static RepositoryContext _repositoryDbContext = new RepositoryContext();
        #endregion

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns></returns>
        public static string GetUserId()
        {
            //return HttpContext.Current.User.Identity.Name;
            var user = _applicationDbContext.Users.Where(m => m.Email == HttpContext.Current.User.Identity.Name).SingleOrDefault();
            return user.Id;
        }

        public static ApplicationUser GetUser()
        {
            var user = _applicationDbContext.Users.Where(m => m.Email == HttpContext.Current.User.Identity.Name).SingleOrDefault();
            return user;
        }

        /// <summary>
        /// Determines whether [is super admin].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is super admin]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSuperAdmin()
        {
            try
            {
                var user = _applicationDbContext.Users.Where(m => m.Email == HttpContext.Current.User.Identity.Name).SingleOrDefault();
                if (user != null)
                {
                    return user.IsSuperAdmin;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static bool CheckPermission(string module, string action)
        {
            try
            {
                var user = _applicationDbContext.Users.Where(m => m.Email == HttpContext.Current.User.Identity.Name).SingleOrDefault();

                if (user.IsSuperAdmin)
                {
                    return true;
                }
                Guid d = Guid.Parse(user.RoleId);
                if (action == "Create" && _repositoryDbContext.RolePermission.Count(_ => (_.RoleId == d) & (_.Create == true) & (_.ModuleName == module)) > 0) { return true; }
                else if (action == "Read" && _repositoryDbContext.RolePermission.Count(_ => (_.RoleId == d) & (_.Read == true) & (_.ModuleName == module)) > 0) { return true; }
                else if (action == "Edit" && _repositoryDbContext.RolePermission.Count(_ => (_.RoleId == d) & (_.Edit == true) & (_.ModuleName == module)) > 0) { return true; }
                else if (action == "Delete" && _repositoryDbContext.RolePermission.Count(_ => (_.RoleId == d) & (_.Delete == true) & (_.ModuleName == module)) > 0) { return true; }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}