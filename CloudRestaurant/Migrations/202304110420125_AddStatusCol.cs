namespace CloudRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Status", c => c.String());
            AddColumn("dbo.Restaurants", "Selected", c => c.Boolean());
            AddColumn("dbo.Restaurants", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", c => c.Int());
            CreateIndex("dbo.DiningTypes", "RestaurantDiningTypesVM_Id");
            AddForeignKey("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", "dbo.Restaurants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", "dbo.Restaurants");
            DropIndex("dbo.DiningTypes", new[] { "RestaurantDiningTypesVM_Id" });
            DropColumn("dbo.DiningTypes", "RestaurantDiningTypesVM_Id");
            DropColumn("dbo.Restaurants", "Discriminator");
            DropColumn("dbo.Restaurants", "Selected");
            DropColumn("dbo.Requests", "Status");
        }
    }
}
