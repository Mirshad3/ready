namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_Add_EndDiscountDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "EndDiscountDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "EndDiscountDate");
        }
    }
}
