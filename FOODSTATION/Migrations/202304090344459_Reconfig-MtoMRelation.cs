namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReconfigMtoMRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RestaurantDiningTypes", "DiningTypeId", "dbo.DiningTypes");
            DropForeignKey("dbo.RestaurantDiningTypes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.ParticipationFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.ParticipationFeatures", "Participation_Id", "dbo.Participations");
            DropForeignKey("dbo.RestaurantParticipations", "ParticipationId", "dbo.Participations");
            DropForeignKey("dbo.RestaurantParticipations", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Restaurants", "Region_Id", "dbo.Regions");
            DropIndex("dbo.Restaurants", new[] { "Region_Id" });
            DropIndex("dbo.RestaurantDiningTypes", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantDiningTypes", new[] { "DiningTypeId" });
            DropIndex("dbo.RestaurantParticipations", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantParticipations", new[] { "ParticipationId" });
            DropIndex("dbo.ParticipationFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ParticipationFeatures", new[] { "Participation_Id" });
            DropColumn("dbo.Restaurants", "RegionId");
            RenameColumn(table: "dbo.Restaurants", name: "Region_Id", newName: "RegionId");
            CreateTable(
                "dbo.DiningTypeRestaurants",
                c => new
                    {
                        DiningType_Id = c.Int(nullable: false),
                        Restaurant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiningType_Id, t.Restaurant_Id })
                .ForeignKey("dbo.DiningTypes", t => t.DiningType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id, cascadeDelete: true)
                .Index(t => t.DiningType_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.FeatureParticipations",
                c => new
                    {
                        Feature_Id = c.Int(nullable: false),
                        Participation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feature_Id, t.Participation_Id })
                .ForeignKey("dbo.Features", t => t.Feature_Id, cascadeDelete: true)
                .ForeignKey("dbo.Participations", t => t.Participation_Id, cascadeDelete: true)
                .Index(t => t.Feature_Id)
                .Index(t => t.Participation_Id);
            
            CreateTable(
                "dbo.ParticipationRestaurants",
                c => new
                    {
                        Participation_Id = c.Int(nullable: false),
                        Restaurant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participation_Id, t.Restaurant_Id })
                .ForeignKey("dbo.Participations", t => t.Participation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id, cascadeDelete: true)
                .Index(t => t.Participation_Id)
                .Index(t => t.Restaurant_Id);
            
            AlterColumn("dbo.Restaurants", "RegionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Restaurants", "RegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Restaurants", "RegionId");
            AddForeignKey("dbo.Restaurants", "RegionId", "dbo.Regions", "Id", cascadeDelete: true);
            DropTable("dbo.RestaurantDiningTypes");
            DropTable("dbo.RestaurantParticipations");
            DropTable("dbo.ParticipationFeatures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParticipationFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        FarticipationId = c.Int(nullable: false),
                        Participation_Id = c.Int(),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantDiningTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        DiningTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Restaurants", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.ParticipationRestaurants", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.ParticipationRestaurants", "Participation_Id", "dbo.Participations");
            DropForeignKey("dbo.FeatureParticipations", "Participation_Id", "dbo.Participations");
            DropForeignKey("dbo.FeatureParticipations", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.DiningTypeRestaurants", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.DiningTypeRestaurants", "DiningType_Id", "dbo.DiningTypes");
            DropIndex("dbo.ParticipationRestaurants", new[] { "Restaurant_Id" });
            DropIndex("dbo.ParticipationRestaurants", new[] { "Participation_Id" });
            DropIndex("dbo.FeatureParticipations", new[] { "Participation_Id" });
            DropIndex("dbo.FeatureParticipations", new[] { "Feature_Id" });
            DropIndex("dbo.DiningTypeRestaurants", new[] { "Restaurant_Id" });
            DropIndex("dbo.DiningTypeRestaurants", new[] { "DiningType_Id" });
            DropIndex("dbo.Restaurants", new[] { "RegionId" });
            AlterColumn("dbo.Restaurants", "RegionId", c => c.Int());
            AlterColumn("dbo.Restaurants", "RegionId", c => c.String(nullable: false));
            DropTable("dbo.ParticipationRestaurants");
            DropTable("dbo.FeatureParticipations");
            DropTable("dbo.DiningTypeRestaurants");
            RenameColumn(table: "dbo.Restaurants", name: "RegionId", newName: "Region_Id");
            AddColumn("dbo.Restaurants", "RegionId", c => c.String(nullable: false));
            CreateIndex("dbo.ParticipationFeatures", "Participation_Id");
            CreateIndex("dbo.ParticipationFeatures", "FeatureId");
            CreateIndex("dbo.RestaurantParticipations", "ParticipationId");
            CreateIndex("dbo.RestaurantParticipations", "RestaurantId");
            CreateIndex("dbo.RestaurantDiningTypes", "DiningTypeId");
            CreateIndex("dbo.RestaurantDiningTypes", "RestaurantId");
            CreateIndex("dbo.Restaurants", "Region_Id");
            AddForeignKey("dbo.Restaurants", "Region_Id", "dbo.Regions", "Id");
            AddForeignKey("dbo.RestaurantParticipations", "RestaurantId", "dbo.Restaurants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RestaurantParticipations", "ParticipationId", "dbo.Participations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParticipationFeatures", "Participation_Id", "dbo.Participations", "Id");
            AddForeignKey("dbo.ParticipationFeatures", "FeatureId", "dbo.Features", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RestaurantDiningTypes", "RestaurantId", "dbo.Restaurants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RestaurantDiningTypes", "DiningTypeId", "dbo.DiningTypes", "Id", cascadeDelete: true);
        }
    }
}
