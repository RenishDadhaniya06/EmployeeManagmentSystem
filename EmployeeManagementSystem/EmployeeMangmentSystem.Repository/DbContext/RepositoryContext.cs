using EmployeeMangmentSystem.Repository.Models;
using System.Data.Entity;

namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
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

        public DbSet<Templates> Templates { get; set; }
    }
}
