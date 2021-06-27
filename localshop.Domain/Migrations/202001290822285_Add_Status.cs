namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "StatusId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "StatusId");
            AddForeignKey("dbo.Products", "StatusId", "dbo.Statuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "StatusId", "dbo.Statuses");
            DropIndex("dbo.Products", new[] { "StatusId" });
            DropColumn("dbo.Products", "StatusId");
            DropTable("dbo.Statuses");
        }
    }
}
