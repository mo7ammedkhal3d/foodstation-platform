namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColsInRestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Restaurants", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Latitude");
            DropColumn("dbo.Restaurants", "longitude");
        }
    }
}
