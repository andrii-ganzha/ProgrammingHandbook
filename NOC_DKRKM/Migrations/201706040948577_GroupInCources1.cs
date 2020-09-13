namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInCources1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupInCources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CouceID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        Cource_CourceID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cources", t => t.Cource_CourceID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.Cource_CourceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupInCources", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupInCources", "Cource_CourceID", "dbo.Cources");
            DropIndex("dbo.GroupInCources", new[] { "Cource_CourceID" });
            DropIndex("dbo.GroupInCources", new[] { "GroupID" });
            DropTable("dbo.GroupInCources");
        }
    }
}
