namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyCources : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Cource_CourceID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Cource_CourceID");
            AddForeignKey("dbo.AspNetUsers", "Cource_CourceID", "dbo.Cources", "CourceID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Cource_CourceID", "dbo.Cources");
            DropIndex("dbo.AspNetUsers", new[] { "Cource_CourceID" });
            DropColumn("dbo.AspNetUsers", "Cource_CourceID");
        }
    }
}
