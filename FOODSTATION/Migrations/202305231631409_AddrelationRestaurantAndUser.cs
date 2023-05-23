namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddrelationRestaurantAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Restaurants", "OwnerId");
            AddForeignKey("dbo.Restaurants", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Restaurants", new[] { "OwnerId" });
            DropColumn("dbo.Restaurants", "OwnerId");
        }
    }
}
