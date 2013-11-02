namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_list_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLists", "ListStatus", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLists", "ListStatus");
        }
    }
}
