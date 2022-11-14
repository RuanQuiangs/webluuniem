namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        Slug = c.String(),
                        Avatar = c.String(),
                        ShowOnHomePage = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        Country = c.String(),
                        CreateOnUtc = c.DateTime(nullable: false),
                        UpdateOnUtc = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Slug = c.String(),
                        Avatar = c.String(),
                        ShowOnHomePage = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        CreateOnUtc = c.DateTime(nullable: false),
                        UpdateOnUtc = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Guid(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        OrderName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatus = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        DescribeShort = c.String(),
                        DescribeFull = c.String(),
                        OEM = c.String(),
                        BrandID = c.String(),
                        Image = c.String(),
                        Amount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Slug = c.String(),
                        Introduce = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Avatar = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropTable("dbo.User");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
        }
    }
}
