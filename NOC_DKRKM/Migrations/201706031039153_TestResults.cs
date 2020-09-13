namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestResults : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.String(maxLength: 128),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            AddColumn("dbo.Answers", "TestResult_ID", c => c.Int());
            CreateIndex("dbo.Answers", "TestResult_ID");
            AddForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "TestResult_ID", "dbo.TestResults");
            DropIndex("dbo.TestResults", new[] { "ApplicationUserID" });
            DropIndex("dbo.Answers", new[] { "TestResult_ID" });
            DropColumn("dbo.Answers", "TestResult_ID");
            DropTable("dbo.TestResults");
        }
    }
}
