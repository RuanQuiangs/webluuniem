namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInit3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Post");
            AddColumn("dbo.Post", "PostTitle", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Post", "PostID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Post", "PostTitle");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Post");
            AlterColumn("dbo.Post", "PostID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Post", "PostTitle");
            AddPrimaryKey("dbo.Post", "PostID");
        }
    }
}
