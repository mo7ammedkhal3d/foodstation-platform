namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterParticiTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participations", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participations", "Price");
        }
    }
}
