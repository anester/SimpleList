namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingItemParts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemParts",
                c => new
                    {
                        ItemPartId = c.Int(nullable: false, identity: true),
                        ListItemId = c.Int(nullable: false),
                        Description = c.String(),
                        OrderNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemPartId)
                .ForeignKey("dbo.ListItems", t => t.ListItemId, cascadeDelete: true)
                .Index(t => t.ListItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemParts", "ListItemId", "dbo.ListItems");
            DropIndex("dbo.ItemParts", new[] { "ListItemId" });
            DropTable("dbo.ItemParts");
        }
    }
}
