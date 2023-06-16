namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdvTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemBills", newName: "BillItems");
            DropPrimaryKey("dbo.BillItems");
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ImgUrl = c.String(),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            AddPrimaryKey("dbo.BillItems", new[] { "Bill_Id", "Item_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Advertisements", new[] { "RestaurantId" });
            DropPrimaryKey("dbo.BillItems");
            DropTable("dbo.Advertisements");
            AddPrimaryKey("dbo.BillItems", new[] { "Item_Id", "Bill_Id" });
            RenameTable(name: "dbo.BillItems", newName: "ItemBills");
        }
    }
}
