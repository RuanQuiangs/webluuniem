namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Post", "User_RePassword");
            DropColumn("dbo.Post", "User_FirstName");
            DropColumn("dbo.Post", "User_LastName");
            DropColumn("dbo.Post", "User_Introduce");
            DropColumn("dbo.Post", "User_Email");
            DropColumn("dbo.Post", "User_Address");
            DropColumn("dbo.Post", "User_Phone");
            DropColumn("dbo.Post", "User_Avatar");
            DropColumn("dbo.Post", "User_Username");
            DropColumn("dbo.Post", "User_Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "User_Password", c => c.String());
            AddColumn("dbo.Post", "User_Username", c => c.String());
            AddColumn("dbo.Post", "User_Avatar", c => c.String());
            AddColumn("dbo.Post", "User_Phone", c => c.String());
            AddColumn("dbo.Post", "User_Address", c => c.String());
            AddColumn("dbo.Post", "User_Email", c => c.String());
            AddColumn("dbo.Post", "User_Introduce", c => c.String());
            AddColumn("dbo.Post", "User_LastName", c => c.String());
            AddColumn("dbo.Post", "User_FirstName", c => c.String());
            AddColumn("dbo.Post", "User_RePassword", c => c.String());
        }
    }
}
