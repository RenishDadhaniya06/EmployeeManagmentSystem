using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Repository.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public static class CommonHelper
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        private static RepositoryContext _repositoryDbContext = new RepositoryContext();

        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.Name;
        }

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