namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Information", "AuthorID");
            AddForeignKey("dbo.Information", "AuthorID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Information", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Information", new[] { "AuthorID" });
            DropColumn("dbo.Information", "AuthorID");
        }
    }
}
