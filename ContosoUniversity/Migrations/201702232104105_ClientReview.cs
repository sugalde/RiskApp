namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionLog", "OutstandingBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TransactionLog", "WorkProgress", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TransactionLog", "InFlight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TransactionLog", "Sum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionLog", "Sum");
            DropColumn("dbo.TransactionLog", "InFlight");
            DropColumn("dbo.TransactionLog", "WorkProgress");
            DropColumn("dbo.TransactionLog", "OutstandingBalance");
        }
    }
}
