namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbMigration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.AspNetUsers", "Group_GroupID");
            AddForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups", "GroupID");
        }
    }
}
