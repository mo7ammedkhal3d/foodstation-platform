namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColsAndRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "DiningTypeId", c => c.Int(nullable: true));
            AddColumn("dbo.Bills", "Location", c => c.String());
            CreateIndex("dbo.Bills", "DiningTypeId");
            AddForeignKey("dbo.Bills", "DiningTypeId", "dbo.DiningTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "DiningTypeId", "dbo.DiningTypes");
            DropIndex("dbo.Bills", new[] { "DiningTypeId" });
            DropColumn("dbo.Bills", "Location");
            DropColumn("dbo.Bills", "DiningTypeId");
        }
    }
}
