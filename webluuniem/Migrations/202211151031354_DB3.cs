namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "User_RePassword", c => c.String());
            AddColumn("dbo.Order", "User_FirstName", c => c.String());
            AddColumn("dbo.Order", "User_LastName", c => c.String());
            AddColumn("dbo.Order", "User_Introduce", c => c.String());
            AddColumn("dbo.Order", "User_Email", c => c.String());
            AddColumn("dbo.Order", "User_Address", c => c.String());
            AddColumn("dbo.Order", "User_Phone", c => c.String());
            AddColumn("dbo.Order", "User_Avatar", c => c.String());
            AddColumn("dbo.Order", "User_Username", c => c.String());
            AddColumn("dbo.Order", "User_Password", c => c.String());
            AddColumn("dbo.Order", "AdrressOrder", c => c.String());
            AddColumn("dbo.Order", "PhoneOrder", c => c.String());
            CreateIndex("dbo.Order", "ProductID");
            AddForeignKey("dbo.Order", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ProductID", "dbo.Product");
            DropIndex("dbo.Order", new[] { "ProductID" });
            DropColumn("dbo.Order", "PhoneOrder");
            DropColumn("dbo.Order", "AdrressOrder");
            DropColumn("dbo.Order", "User_Password");
            DropColumn("dbo.Order", "User_Username");
            DropColumn("dbo.Order", "User_Avatar");
            DropColumn("dbo.Order", "User_Phone");
            DropColumn("dbo.Order", "User_Address");
            DropColumn("dbo.Order", "User_Email");
            DropColumn("dbo.Order", "User_Introduce");
            DropColumn("dbo.Order", "User_LastName");
            DropColumn("dbo.Order", "User_FirstName");
            DropColumn("dbo.Order", "User_RePassword");
            DropColumn("dbo.Order", "UserID");
        }
    }
}
