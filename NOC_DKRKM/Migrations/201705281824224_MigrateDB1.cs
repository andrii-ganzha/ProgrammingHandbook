namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        InformationID = c.Int(nullable: false, identity: true),
                        Head = c.String(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.InformationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Information");
        }
    }
}
