namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GroupID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GroupID");
        }
    }
}
