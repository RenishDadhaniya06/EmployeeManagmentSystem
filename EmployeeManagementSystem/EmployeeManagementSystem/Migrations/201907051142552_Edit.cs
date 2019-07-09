namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
        }
    }
}
