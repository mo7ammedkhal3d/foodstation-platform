namespace CloudRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataBaseTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantDiningTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        DiningTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiningTypes", t => t.DiningTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.DiningTypeId);
            
            CreateTable(
                "dbo.DiningTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantParticipations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        ParticipationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participations", t => t.ParticipationId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.ParticipationId);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParticipationFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        FarticipationId = c.Int(nullable: false),
                        Participation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Participations", t => t.Participation_Id)
                .Index(t => t.FeatureId)
                .Index(t => t.Participation_Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Restaurants", "RegionName", c => c.String(nullable: false));
            AddColumn("dbo.Restaurants", "Region_Id", c => c.Int());
            CreateIndex("dbo.Restaurants", "Region_Id");
            AddForeignKey("dbo.Restaurants", "Region_Id", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantParticipations", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantParticipations", "ParticipationId", "dbo.Participations");
            DropForeignKey("dbo.ParticipationFeatures", "Participation_Id", "dbo.Participations");
            DropForeignKey("dbo.ParticipationFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.RestaurantDiningTypes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantDiningTypes", "DiningTypeId", "dbo.DiningTypes");
            DropForeignKey("dbo.Restaurants", "Region_Id", "dbo.Regions");
            DropForeignKey("dbo.Regions", "Country_Id", "dbo.Countries");
            DropIndex("dbo.ParticipationFeatures", new[] { "Participation_Id" });
            DropIndex("dbo.ParticipationFeatures", new[] { "FeatureId" });
            DropIndex("dbo.RestaurantParticipations", new[] { "ParticipationId" });
            DropIndex("dbo.RestaurantParticipations", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantDiningTypes", new[] { "DiningTypeId" });
            DropIndex("dbo.RestaurantDiningTypes", new[] { "RestaurantId" });
            DropIndex("dbo.Regions", new[] { "Country_Id" });
            DropIndex("dbo.Restaurants", new[] { "Region_Id" });
            DropColumn("dbo.Restaurants", "Region_Id");
            DropColumn("dbo.Restaurants", "RegionName");
            DropTable("dbo.Features");
            DropTable("dbo.ParticipationFeatures");
            DropTable("dbo.Participations");
            DropTable("dbo.RestaurantParticipations");
            DropTable("dbo.DiningTypes");
            DropTable("dbo.RestaurantDiningTypes");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
        }
    }
}
