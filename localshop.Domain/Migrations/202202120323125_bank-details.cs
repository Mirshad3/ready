namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bankdetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        BankName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AccountName = c.String(),
                        Branch = c.String(),
                        AccountNumber = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ReturnCashes", "OrderWaybillid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReturnCashes", "OrderWaybillid");
            DropTable("dbo.BankAccounts");
        }
    }
}
