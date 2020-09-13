namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestResults3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentID)
                .Index(t => t.StudentID);
            
            AddColumn("dbo.Answers", "Test_TestID", c => c.Int());
            AddColumn("dbo.Questions", "Test_TestID", c => c.Int());
            CreateIndex("dbo.Answers", "Test_TestID");
            CreateIndex("dbo.Questions", "Test_TestID");
            AddForeignKey("dbo.Answers", "Test_TestID", "dbo.Tests", "TestID");
            AddForeignKey("dbo.Questions", "Test_TestID", "dbo.Tests", "TestID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "StudentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Test_TestID", "dbo.Tests");
            DropForeignKey("dbo.Answers", "Test_TestID", "dbo.Tests");
            DropIndex("dbo.Tests", new[] { "StudentID" });
            DropIndex("dbo.Questions", new[] { "Test_TestID" });
            DropIndex("dbo.Answers", new[] { "Test_TestID" });
            DropColumn("dbo.Questions", "Test_TestID");
            DropColumn("dbo.Answers", "Test_TestID");
            DropTable("dbo.Tests");
        }
    }
}
