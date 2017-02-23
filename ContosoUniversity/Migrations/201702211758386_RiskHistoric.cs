namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RiskHistoric : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Risk", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Risk", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Risk", "Created");
            DropColumn("dbo.Risk", "CreatedBy");
        }
    }
}
