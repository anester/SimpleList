namespace SimpleListContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingItemParts01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemParts", "ListItemId", "dbo.ListItems");
            DropIndex("dbo.ItemParts", new[] { "ListItemId" });
            DropTable("dbo.ItemParts");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ItemPartId);
            
            CreateIndex("dbo.ItemParts", "ListItemId");
            AddForeignKey("dbo.ItemParts", "ListItemId", "dbo.ListItems", "ListItemId", cascadeDelete: true);
        }
    }
}
