namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Whishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Whishlists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Whishlists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Whishlists", "ProductId", "dbo.Products");
            DropIndex("dbo.Whishlists", new[] { "ProductId" });
            DropIndex("dbo.Whishlists", new[] { "UserId" });
            DropTable("dbo.Whishlists");
        }
    }
}
