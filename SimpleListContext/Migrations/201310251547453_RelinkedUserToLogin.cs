namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelinkedUserToLogin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropIndex("dbo.Logins", new[] { "UserId" });
            AddColumn("dbo.Users", "LoginId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Users", "LoginId", "dbo.Logins", "LoginId", cascadeDelete: true);
            CreateIndex("dbo.Users", "LoginId");
            DropColumn("dbo.Users", "UserLogin");
            DropColumn("dbo.Logins", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserLogin", c => c.String());
            DropIndex("dbo.Users", new[] { "LoginId" });
            DropForeignKey("dbo.Users", "LoginId", "dbo.Logins");
            DropColumn("dbo.Users", "LoginId");
            CreateIndex("dbo.Logins", "UserId");
            AddForeignKey("dbo.Logins", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
