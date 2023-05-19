namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestaurantNoCol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regions", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Regions", new[] { "Country_Id" });
            RenameColumn(table: "dbo.Regions", name: "Country_Id", newName: "CountryId");
            AddColumn("dbo.Regions", "RestaurantNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Regions", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Regions", "CountryId");
            AddForeignKey("dbo.Regions", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropIndex("dbo.Regions", new[] { "CountryId" });
            AlterColumn("dbo.Regions", "CountryId", c => c.Int());
            DropColumn("dbo.Regions", "RestaurantNo");
            RenameColumn(table: "dbo.Regions", name: "CountryId", newName: "Country_Id");
            CreateIndex("dbo.Regions", "Country_Id");
            AddForeignKey("dbo.Regions", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
