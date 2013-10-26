namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinglogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoginName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.LoginId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropTable("dbo.Logins");
        }
    }
}
