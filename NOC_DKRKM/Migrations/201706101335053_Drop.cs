namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults");
            DropForeignKey("dbo.TestResults", "StudentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "Test_TestID", "dbo.Tests");
            DropForeignKey("dbo.Questions", "Test_TestID", "dbo.Tests");
            DropForeignKey("dbo.Tests", "StudentID", "dbo.AspNetUsers");
            DropIndex("dbo.Answers", new[] { "TestResult_ID" });
            DropIndex("dbo.Answers", new[] { "Test_TestID" });
            DropIndex("dbo.Questions", new[] { "Test_TestID" });
            DropIndex("dbo.TestResults", new[] { "StudentID" });
            DropIndex("dbo.Tests", new[] { "StudentID" });
            DropColumn("dbo.Answers", "TestResult_ID");
            DropColumn("dbo.Answers", "Test_TestID");
            DropColumn("dbo.Questions", "Test_TestID");
            DropTable("dbo.TestResults");
            DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Questions", "Test_TestID", c => c.Int());
            AddColumn("dbo.Answers", "Test_TestID", c => c.Int());
            AddColumn("dbo.Answers", "TestResult_ID", c => c.Int());
            CreateIndex("dbo.Tests", "StudentID");
            CreateIndex("dbo.TestResults", "StudentID");
            CreateIndex("dbo.Questions", "Test_TestID");
            CreateIndex("dbo.Answers", "Test_TestID");
            CreateIndex("dbo.Answers", "TestResult_ID");
            AddForeignKey("dbo.Tests", "StudentID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "Test_TestID", "dbo.Tests", "TestID");
            AddForeignKey("dbo.Answers", "Test_TestID", "dbo.Tests", "TestID");
            AddForeignKey("dbo.TestResults", "StudentID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults", "ID");
        }
    }
}
