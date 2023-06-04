namespace FOODSTATION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableBill : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bills", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bills", "UserId");
            AddForeignKey("dbo.Bills", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bills", new[] { "UserId" });
            AlterColumn("dbo.Bills", "UserId", c => c.String());
        }
    }
}
