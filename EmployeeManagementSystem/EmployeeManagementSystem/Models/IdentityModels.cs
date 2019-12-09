using EmployeeManagementSystem.Models.Time;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// ApplicationUser
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsSuperAdmin { get; set; }

        public Guid ParentUserID { get; set; }

        //[Display(Name = "lblIsActive", ResourceType = typeof(RegestrationResources))]
        //public bool IsActive { get; set; }

        public Status UserStatus { get; set; }

        public string RoleId { get; set; }
    }
   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
       
        public DbSet<TimeTrackingSystems> TimeTrackingSystems { get; set; }

        public DbSet<TimeTrackings> TimeTrackings { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}