namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        StudentID = c.String(nullable: false, maxLength: 128),
                        AnswerID = c.Int(nullable: false),
                        Change = c.Boolean(nullable: false),
                        k = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.AnswerID })
                .ForeignKey("dbo.Answers", t => t.AnswerID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.AnswerID);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TopicID = c.Int(nullable: false),
                        StudentID = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID)
                .Index(t => t.StudentID);
            
            AddColumn("dbo.Answers", "TestResult_ID", c => c.Int());
            AddColumn("dbo.Questions", "TestResult_ID", c => c.Int());
            CreateIndex("dbo.Answers", "TestResult_ID");
            CreateIndex("dbo.Questions", "TestResult_ID");
            AddForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults", "ID");
            AddForeignKey("dbo.Questions", "TestResult_ID", "dbo.TestResults", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.TestResults", "StudentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "TestResult_ID", "dbo.TestResults");
            DropForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults");
            DropForeignKey("dbo.StudentAnswers", "StudentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentAnswers", "AnswerID", "dbo.Answers");
            DropIndex("dbo.TestResults", new[] { "StudentID" });
            DropIndex("dbo.TestResults", new[] { "TopicID" });
            DropIndex("dbo.StudentAnswers", new[] { "AnswerID" });
            DropIndex("dbo.StudentAnswers", new[] { "StudentID" });
            DropIndex("dbo.Questions", new[] { "TestResult_ID" });
            DropIndex("dbo.Answers", new[] { "TestResult_ID" });
            DropColumn("dbo.Questions", "TestResult_ID");
            DropColumn("dbo.Answers", "TestResult_ID");
            DropTable("dbo.TestResults");
            DropTable("dbo.StudentAnswers");
        }
    }
}
