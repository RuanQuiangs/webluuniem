namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "DateFeedback", c => c.DateTime(nullable: false));
            AddColumn("dbo.Review", "DateReview", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "DateReview");
            DropColumn("dbo.Feedbacks", "DateFeedback");
        }
    }
}
