namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_dates_to_userlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLists", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserLists", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLists", "DateCompleted");
            DropColumn("dbo.UserLists", "DateCreated");
        }
    }
}
