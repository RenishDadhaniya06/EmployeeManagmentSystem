namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsSuperAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "ParentUserID", c => c.Guid(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsActive");
            DropColumn("dbo.AspNetUsers", "ParentUserID");
            DropColumn("dbo.AspNetUsers", "IsSuperAdmin");
        }
    }
}
