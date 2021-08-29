namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Weight", c => c.Int());
            AddColumn("dbo.Products", "Width", c => c.Int());
            AddColumn("dbo.Products", "Hight", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Hight");
            DropColumn("dbo.Products", "Width");
            DropColumn("dbo.Products", "Weight");
        }
    }
}
