namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Description");
        }
    }
}
