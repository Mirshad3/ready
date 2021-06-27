namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class team : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UserId");
        }
    }
}
