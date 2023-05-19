namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatusCol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", "dbo.Restaurants");
            DropIndex("dbo.DiningTypes", new[] { "RestaurantDiningTypesVM_Id" });
            DropColumn("dbo.Requests", "Status");
            DropColumn("dbo.Restaurants", "Selected");
            DropColumn("dbo.Restaurants", "Discriminator");
            DropColumn("dbo.DiningTypes", "RestaurantDiningTypesVM_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", c => c.Int());
            AddColumn("dbo.Restaurants", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Restaurants", "Selected", c => c.Boolean());
            AddColumn("dbo.Requests", "Status", c => c.String());
            CreateIndex("dbo.DiningTypes", "RestaurantDiningTypesVM_Id");
            AddForeignKey("dbo.DiningTypes", "RestaurantDiningTypesVM_Id", "dbo.Restaurants", "Id");
        }
    }
}
