using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public static class CommonHelper
    {
        private static ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static bool IsSuperAdmin()
        {
            var user = _applicationDbContext.Users.Where(m => m.Id == HttpContext.Current.User.Identity.Name).SingleOrDefault();

            return user.IsSuperAdmin;
        }
    }
}