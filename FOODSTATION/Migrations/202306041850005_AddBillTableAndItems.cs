namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBillTableAndItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "ItemId", "dbo.Items");
            DropIndex("dbo.Requests", new[] { "ItemId" });
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Bill_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bill_Id, t.Item_Id })
                .ForeignKey("dbo.Bills", t => t.Bill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.Bill_Id)
                .Index(t => t.Item_Id);
            
            DropTable("dbo.Requests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.BillItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.BillItems", "Bill_Id", "dbo.Bills");
            DropIndex("dbo.BillItems", new[] { "Item_Id" });
            DropIndex("dbo.BillItems", new[] { "Bill_Id" });
            DropTable("dbo.BillItems");
            DropTable("dbo.Bills");
            CreateIndex("dbo.Requests", "ItemId");
            AddForeignKey("dbo.Requests", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
    }
}
