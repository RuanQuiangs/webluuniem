namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tests",
                c => new
                    {
                        testId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.testId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tests");
        }
    }
}
