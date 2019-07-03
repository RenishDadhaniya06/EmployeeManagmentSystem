namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserStatus");
        }
    }
}
