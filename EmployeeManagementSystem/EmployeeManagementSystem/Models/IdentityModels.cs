using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DHTMLX.Scheduler;
using EmployeeMangmentSystem.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        public string Description { get; set; }

        [DHXJson(Alias = "start_date")]
        public DateTime StartDate { get; set; }

        [DHXJson(Alias = "end_date")]
        public DateTime EndDate { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}