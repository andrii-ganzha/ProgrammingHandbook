namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "dfdf", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "dfdf");
        }
    }
}
