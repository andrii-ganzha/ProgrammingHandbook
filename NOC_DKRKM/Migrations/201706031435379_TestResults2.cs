namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestResults2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Check", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Check");
        }
    }
}
