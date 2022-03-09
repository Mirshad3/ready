namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class returncash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ShopName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ShopName");
        }
    }
}
