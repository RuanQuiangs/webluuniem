namespace webluuniem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        Text = c.String(),
                        DateComment = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        email = c.String(),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReViewID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Star = c.Byte(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.ReViewID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Review");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Comment");
        }
    }
}
