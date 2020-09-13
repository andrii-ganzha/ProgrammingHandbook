namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "Text", c => c.Int(nullable: false));
        }
    }
}
