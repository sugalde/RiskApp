namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inflight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InflightOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LogNumber = c.Int(nullable: false),
                        FleetNumber = c.Int(nullable: false),
                        QuotationNumber = c.Int(nullable: false),
                        UnitNumber = c.Int(nullable: false),
                        AmountOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InflightOrder");
        }
    }
}
