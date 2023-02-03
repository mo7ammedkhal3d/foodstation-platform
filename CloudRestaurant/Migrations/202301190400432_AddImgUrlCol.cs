namespace CloudRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgUrlCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImgUrl");
        }
    }
}
