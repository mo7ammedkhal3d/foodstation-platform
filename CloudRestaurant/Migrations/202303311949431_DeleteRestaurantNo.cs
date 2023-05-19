namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRestaurantNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "RegionId", c => c.String(nullable: false));
            DropColumn("dbo.Restaurants", "RegionName");
            DropColumn("dbo.Regions", "RestaurantNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Regions", "RestaurantNo", c => c.Int(nullable: false));
            AddColumn("dbo.Restaurants", "RegionName", c => c.String(nullable: false));
            DropColumn("dbo.Restaurants", "RegionId");
        }
    }
}
