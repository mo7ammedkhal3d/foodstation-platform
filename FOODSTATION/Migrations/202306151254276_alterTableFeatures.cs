namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableFeatures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Features", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Features", "Numb", c => c.Int(nullable: false));
            DropColumn("dbo.Features", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Features", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Features", "Numb");
            DropColumn("dbo.Features", "Name");
        }
    }
}
