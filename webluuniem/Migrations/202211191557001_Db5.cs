namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Brand_BrandID", c => c.Int());
            CreateIndex("dbo.Product", "Brand_BrandID");
            AddForeignKey("dbo.Product", "Brand_BrandID", "dbo.Brand", "BrandID");
            DropColumn("dbo.Product", "OEM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "OEM", c => c.String());
            DropForeignKey("dbo.Product", "Brand_BrandID", "dbo.Brand");
            DropIndex("dbo.Product", new[] { "Brand_BrandID" });
            DropColumn("dbo.Product", "Brand_BrandID");
        }
    }
}
