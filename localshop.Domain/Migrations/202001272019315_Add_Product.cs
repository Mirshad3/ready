namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Sku = c.String(nullable: false, maxLength: 450),
                        MetaTitle = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Detail = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Sku, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "Sku" });
            DropTable("dbo.Products");
        }
    }
}
