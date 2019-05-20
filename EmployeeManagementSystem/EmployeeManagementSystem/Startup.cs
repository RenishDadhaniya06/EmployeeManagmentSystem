using EmployeeManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(EmployeeManagementSystem.Startup))]
namespace EmployeeManagementSystem
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
    
}
