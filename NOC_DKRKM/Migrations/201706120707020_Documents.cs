namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TopicID = c.Int(nullable: false),
                        Fail = c.String(nullable: false),
                        Describe = c.String(),
                        KindID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentID)
                .ForeignKey("dbo.Kinds", t => t.KindID, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID)
                .Index(t => t.KindID);
            
            CreateTable(
                "dbo.Kinds",
                c => new
                    {
                        KindId = c.Int(nullable: false, identity: true),
                        KindName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KindId);
            
            AddColumn("dbo.Topics", "Describe", c => c.String());
            AddColumn("dbo.Cources", "Describe", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Documents", "KindID", "dbo.Kinds");
            DropIndex("dbo.Documents", new[] { "KindID" });
            DropIndex("dbo.Documents", new[] { "TopicID" });
            DropColumn("dbo.Cources", "Describe");
            DropColumn("dbo.Topics", "Describe");
            DropTable("dbo.Kinds");
            DropTable("dbo.Documents");
        }
    }
}
