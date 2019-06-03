using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public static class CommonHelper
    {
        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}