namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "dfdf");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "dfdf", c => c.String());
        }
    }
}
