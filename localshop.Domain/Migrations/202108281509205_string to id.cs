namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringtoid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Zone = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cities");
        }
    }
}
