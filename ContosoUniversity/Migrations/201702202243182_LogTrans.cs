namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogTrans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FleetNumber = c.Int(nullable: false),
                        QuotationID = c.Int(nullable: false),
                        CreditLineInitial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuotationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(maxLength: 50),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionLog");
        }
    }
}
