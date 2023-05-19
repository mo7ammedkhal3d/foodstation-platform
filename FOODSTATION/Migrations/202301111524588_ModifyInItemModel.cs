namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyInItemModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ImgUrl", c => c.String(nullable: false));
        }
    }
}
