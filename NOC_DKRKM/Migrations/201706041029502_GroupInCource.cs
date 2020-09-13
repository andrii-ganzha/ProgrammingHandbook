namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInCource : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupInCources", "Cource_CourceID", "dbo.Cources");
            DropIndex("dbo.GroupInCources", new[] { "Cource_CourceID" });
            RenameColumn(table: "dbo.GroupInCources", name: "Cource_CourceID", newName: "CourceID");
            AlterColumn("dbo.GroupInCources", "CourceID", c => c.Int(nullable: false));
            CreateIndex("dbo.GroupInCources", "CourceID");
            AddForeignKey("dbo.GroupInCources", "CourceID", "dbo.Cources", "CourceID", cascadeDelete: true);
            DropColumn("dbo.GroupInCources", "CouceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupInCources", "CouceID", c => c.Int(nullable: false));
            DropForeignKey("dbo.GroupInCources", "CourceID", "dbo.Cources");
            DropIndex("dbo.GroupInCources", new[] { "CourceID" });
            AlterColumn("dbo.GroupInCources", "CourceID", c => c.Int());
            RenameColumn(table: "dbo.GroupInCources", name: "CourceID", newName: "Cource_CourceID");
            CreateIndex("dbo.GroupInCources", "Cource_CourceID");
            AddForeignKey("dbo.GroupInCources", "Cource_CourceID", "dbo.Cources", "CourceID");
        }
    }
}
