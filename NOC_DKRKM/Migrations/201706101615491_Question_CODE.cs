namespace NOC_DKRKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Question_CODE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Code");
        }
    }
}
