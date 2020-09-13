namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Question_Picture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Image");
        }
    }
}
