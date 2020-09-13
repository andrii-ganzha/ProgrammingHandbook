namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInCource1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GroupInCources");
            AddPrimaryKey("dbo.GroupInCources", new[] { "CourceID", "GroupID" });
            DropColumn("dbo.GroupInCources", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupInCources", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.GroupInCources");
            AddPrimaryKey("dbo.GroupInCources", "ID");
        }
    }
}
