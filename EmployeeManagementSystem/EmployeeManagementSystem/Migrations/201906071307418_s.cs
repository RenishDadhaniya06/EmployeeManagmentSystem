namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "Role");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRole");
            RenameTable(name: "dbo.AspNetUsers", newName: "UserDetails");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaim");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogin");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserDetails", "UserNameIndex");
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            AddColumn("dbo.UserRole", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserClaim", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserLogin", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserClaim", "UserId", c => c.String());
            CreateIndex("dbo.UserRole", "IdentityUser_Id");
            CreateIndex("dbo.UserClaim", "IdentityUser_Id");
            CreateIndex("dbo.UserLogin", "IdentityUser_Id");
            CreateIndex("dbo.UserDetails", "Id");
            AddForeignKey("dbo.UserDetails", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserClaim", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogin", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRole", "IdentityUser_Id", "dbo.Users", "Id");
            DropColumn("dbo.UserDetails", "Email");
            DropColumn("dbo.UserDetails", "EmailConfirmed");
            DropColumn("dbo.UserDetails", "PasswordHash");
            DropColumn("dbo.UserDetails", "SecurityStamp");
            DropColumn("dbo.UserDetails", "PhoneNumber");
            DropColumn("dbo.UserDetails", "PhoneNumberConfirmed");
            DropColumn("dbo.UserDetails", "TwoFactorEnabled");
            DropColumn("dbo.UserDetails", "LockoutEndDateUtc");
            DropColumn("dbo.UserDetails", "LockoutEnabled");
            DropColumn("dbo.UserDetails", "AccessFailedCount");
            DropColumn("dbo.UserDetails", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDetails", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.UserDetails", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.UserDetails", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDetails", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.UserDetails", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDetails", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDetails", "PhoneNumber", c => c.String());
            AddColumn("dbo.UserDetails", "SecurityStamp", c => c.String());
            AddColumn("dbo.UserDetails", "PasswordHash", c => c.String());
            AddColumn("dbo.UserDetails", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDetails", "Email", c => c.String(maxLength: 256));
            DropForeignKey("dbo.UserRole", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogin", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaim", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserDetails", "Id", "dbo.Users");
            DropIndex("dbo.UserDetails", new[] { "Id" });
            DropIndex("dbo.UserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRole", new[] { "IdentityUser_Id" });
            AlterColumn("dbo.UserClaim", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserLogin", "IdentityUser_Id");
            DropColumn("dbo.UserClaim", "IdentityUser_Id");
            DropColumn("dbo.UserRole", "IdentityUser_Id");
            DropTable("dbo.Users");
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserDetails", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.UserRole", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.UserLogin", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.UserClaim", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.UserDetails", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRole", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Role", newName: "AspNetRoles");
        }
    }
}
