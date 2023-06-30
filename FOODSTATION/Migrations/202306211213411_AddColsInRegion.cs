namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColsInRegion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Regions", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Regions", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Regions", "Latitude");
            DropColumn("dbo.Regions", "Longitude");
        }
    }
}
