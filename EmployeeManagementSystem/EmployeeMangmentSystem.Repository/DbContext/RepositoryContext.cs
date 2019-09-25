
namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using System.Data.Entity;
    #endregion


    /// <summary>
    /// Repository Context
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="EmployeeMangmentSystem.Repository.StoreProcedureService.IDataRepositoryContext" />
    public partial class RepositoryContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryContext"/> class.
        /// </summary>
        public RepositoryContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the designations.
        /// </summary>
        /// <value>
        /// The designations.
        /// </value>
        public DbSet<Designation> Designations { get; set; }

        public DbSet<Departments> Departments { get; set; }

        public DbSet<Technologies> Technologies { get; set; }

        public DbSet<Templates> Templates { get; set; }

        public DbSet<TemplatesType> TemplatesType { get; set; }

        public DbSet<Countries> Countries { get; set; }

        public DbSet<States> States { get; set; }

        public DbSet<PermissionModules> PermissionModules { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<RolesViewModel> RolesViewModel { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<SettingView> SettingView { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<Skills> Skills { get; set; }

        public DbSet<Notifications> Notifications { get; set; }

        public DbSet<Openings> Openings { get; set; }

        public DbSet<Candidates> Candidate { get; set; }

        public DbSet<CandidateSkills> CandidateSkills { get; set; }

        public DbSet<CandidateTechnologies> CandidateTechnologies { get; set; }

        public DbSet<Interviewers> Interviewers { get; set; }

        public DbSet<Interviews> Interviews { get; set; }



        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
