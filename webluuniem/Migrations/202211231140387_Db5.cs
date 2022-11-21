namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Post");
            AddColumn("dbo.Post", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Post", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Post", "PostTitle", c => c.String());
            AlterColumn("dbo.Post", "PostID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Post", "PostID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Post");
            AlterColumn("dbo.Post", "PostID", c => c.Int(nullable: false));
            AlterColumn("dbo.Post", "PostTitle", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Post", "Deleted");
            DropColumn("dbo.Post", "CreateDate");
            AddPrimaryKey("dbo.Post", "PostTitle");
        }
    }
}
