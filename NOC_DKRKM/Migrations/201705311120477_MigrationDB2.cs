namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                {
                    GroupID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.GroupID);

            AddColumn("dbo.AspNetUsers", "name", c => c.String());
            AddColumn("dbo.AspNetUsers", "sname", c => c.String());
            AddColumn("dbo.AspNetUsers", "lname", c => c.String());
            AddColumn("dbo.AspNetUsers", "GroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "GroupID");
            AddForeignKey("dbo.AspNetUsers", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "GroupID", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "GroupID" });
            DropColumn("dbo.AspNetUsers", "GroupID");
            DropColumn("dbo.AspNetUsers", "lname");
            DropColumn("dbo.AspNetUsers", "sname");
            DropColumn("dbo.AspNetUsers", "name");
            DropTable("dbo.Groups");
        }
    }
}
