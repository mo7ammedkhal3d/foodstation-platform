namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterMMRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BillItems", "Bill_Id", "dbo.Bills");
            DropForeignKey("dbo.BillItems", "Item_Id", "dbo.Items");
            DropIndex("dbo.BillItems", new[] { "Bill_Id" });
            DropIndex("dbo.BillItems", new[] { "Item_Id" });
            DropTable("dbo.BillItems");
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.BillId);
            

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Bill_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bill_Id, t.Item_Id });
            
            DropForeignKey("dbo.BillItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.BillItems", new[] { "ItemId" });
            DropTable("dbo.BillItems");
            CreateIndex("dbo.BillItems", "Item_Id");
            CreateIndex("dbo.BillItems", "Bill_Id");
            AddForeignKey("dbo.BillItems", "Item_Id", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BillItems", "Bill_Id", "dbo.Bills", "Id", cascadeDelete: true);
        }
    }
}
