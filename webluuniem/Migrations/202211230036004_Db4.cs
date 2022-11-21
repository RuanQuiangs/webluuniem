namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Slug", c => c.String());
            AddColumn("dbo.Post", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "Slug");
            DropColumn("dbo.Product", "Slug");
        }
    }
}
