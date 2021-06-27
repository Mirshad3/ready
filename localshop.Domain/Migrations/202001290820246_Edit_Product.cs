namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDesciption", c => c.String());
            AddColumn("dbo.Products", "LongDescription", c => c.String());
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Products", "Detail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Detail", c => c.String());
            AddColumn("dbo.Products", "Description", c => c.String());
            DropColumn("dbo.Products", "LongDescription");
            DropColumn("dbo.Products", "ShortDesciption");
        }
    }
}
