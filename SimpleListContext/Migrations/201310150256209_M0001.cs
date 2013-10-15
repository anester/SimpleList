namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M0001 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ListItems", "DateDone", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ListItems", "DateDone", c => c.DateTime(nullable: false));
        }
    }
}
