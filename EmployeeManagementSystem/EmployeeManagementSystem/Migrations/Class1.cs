using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration1 : DbMigrationsConfiguration<EmployeeMangmentSystem.Repository.Repository.Classes.RepositoryContext>
    {
        public Configuration1()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EmployeeMangmentSystem.Repository.Repository.Classes.RepositoryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}