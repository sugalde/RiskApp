namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewRisk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Risk",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EconomicGroup = c.String(maxLength: 50),
                        ParentName = c.String(maxLength: 50),
                        FleetNumber = c.Int(nullable: false),
                        CreditLine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(maxLength: 3),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Obligor = c.String(maxLength: 50),
                        ExpirationDate = c.DateTime(nullable: false),
                        OutstandingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkProgress = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InFlight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Risk");
        }
    }
}
