namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class returncashes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnCashes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderId = c.String(nullable: false),
                        UserId = c.String(),
                        ProductId = c.String(maxLength: 128),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        ReturnDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        Reason = c.String(),
                        OtherReason = c.String(),
                        Image1 = c.String(),
                        Image2 = c.String(),
                        Image3 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnCashes", "ProductId", "dbo.Products");
            DropIndex("dbo.ReturnCashes", new[] { "ProductId" });
            DropTable("dbo.ReturnCashes");
        }
    }
}
