namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "DescribeShort", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "DescribeShort", c => c.String());
        }
    }
}
