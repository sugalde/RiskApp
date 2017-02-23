namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creditlimit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionLog", "RequestStatus", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionLog", "RequestStatus");
        }
    }
}
