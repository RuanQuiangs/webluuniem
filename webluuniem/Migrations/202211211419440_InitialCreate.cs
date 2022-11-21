namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        UserID = c.Int(nullable: false),
                        User_RePassword = c.String(),
                        User_FirstName = c.String(),
                        User_LastName = c.String(),
                        User_Introduce = c.String(),
                        User_Email = c.String(),
                        User_Address = c.String(),
                        User_Phone = c.String(),
                        User_Avatar = c.String(),
                        User_Username = c.String(),
                        User_Password = c.String(),
                        OrderName = c.String(),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdrressOrder = c.String(),
                        PhoneOrder = c.String(),
                        OrderStatus = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        DescribeShort = c.String(maxLength: 200),
                        DescribeFull = c.String(),
                        BrandID = c.Int(nullable: false),
                        Image = c.String(),
                        Amount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brand", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostText = c.String(),
                        Image = c.String(),
                        UserID = c.Int(nullable: false),
                        User_RePassword = c.String(),
                        User_FirstName = c.String(),
                        User_LastName = c.String(),
                        User_Introduce = c.String(),
                        User_Email = c.String(),
                        User_Address = c.String(),
                        User_Phone = c.String(),
                        User_Avatar = c.String(),
                        User_Username = c.String(),
                        User_Password = c.String(),
                    })
                .PrimaryKey(t => t.PostID);
            
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
            DropForeignKey("dbo.Order", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Product", "BrandID", "dbo.Brand");
            DropIndex("dbo.Product", new[] { "BrandID" });
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropIndex("dbo.Order", new[] { "ProductID" });
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
        }
    }
}
