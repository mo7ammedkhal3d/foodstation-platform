namespace CloudRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyItemT : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Items", new[] { "restaurant_Id" });
            RenameColumn(table: "dbo.Items", name: "restaurant_Id", newName: "RestaurantId");
            AlterColumn("dbo.Items", "RestaurantId", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "RestaurantId");
            AddForeignKey("dbo.Items", "RestaurantId", "dbo.Restaurants", "Id", cascadeDelete: true);
            DropColumn("dbo.Items", "ResId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ResId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Items", new[] { "RestaurantId" });
            AlterColumn("dbo.Items", "RestaurantId", c => c.Int());
            RenameColumn(table: "dbo.Items", name: "RestaurantId", newName: "restaurant_Id");
            CreateIndex("dbo.Items", "restaurant_Id");
            AddForeignKey("dbo.Items", "restaurant_Id", "dbo.Restaurants", "Id");
        }
    }
}
