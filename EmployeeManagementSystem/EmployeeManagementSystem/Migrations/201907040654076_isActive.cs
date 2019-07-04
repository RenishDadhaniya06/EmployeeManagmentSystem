namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isActive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
