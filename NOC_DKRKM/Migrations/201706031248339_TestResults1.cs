namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestResults1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TestResults", name: "ApplicationUserID", newName: "StudentID");
            RenameIndex(table: "dbo.TestResults", name: "IX_ApplicationUserID", newName: "IX_StudentID");
            AddColumn("dbo.Answers", "value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "value");
            RenameIndex(table: "dbo.TestResults", name: "IX_StudentID", newName: "IX_ApplicationUserID");
            RenameColumn(table: "dbo.TestResults", name: "StudentID", newName: "ApplicationUserID");
        }
    }
}
