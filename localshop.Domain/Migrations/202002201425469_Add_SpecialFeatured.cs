namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SpecialFeatured : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialFeatureds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Link = c.String(nullable: false),
                        BackgroundImage = c.String(nullable: false),
                        ProductImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpecialFeatureds");
        }
    }
}
