namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WaybillId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderWaybillid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderWaybillid");
        }
    }
}
