namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserLogin = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserLists",
                c => new
                    {
                        UserListId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserListName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UserListId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ListItems",
                c => new
                    {
                        ListItemId = c.Int(nullable: false, identity: true),
                        UserListId = c.Int(nullable: false),
                        ListItemName = c.String(),
                        Description = c.String(),
                        DateEntered = c.DateTime(nullable: false),
                        DateDone = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ListItemId)
                .ForeignKey("dbo.UserLists", t => t.UserListId, cascadeDelete: true)
                .Index(t => t.UserListId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ListItems", new[] { "UserListId" });
            DropIndex("dbo.UserLists", new[] { "UserId" });
            DropForeignKey("dbo.ListItems", "UserListId", "dbo.UserLists");
            DropForeignKey("dbo.UserLists", "UserId", "dbo.Users");
            DropTable("dbo.ListItems");
            DropTable("dbo.UserLists");
            DropTable("dbo.Users");
        }
    }
}
