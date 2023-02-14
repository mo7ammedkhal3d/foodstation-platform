namespace CloudRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ItemId", "dbo.Items");
            DropIndex("dbo.Requests", new[] { "ItemId" });
            DropTable("dbo.Requests");
        }
    }
}
