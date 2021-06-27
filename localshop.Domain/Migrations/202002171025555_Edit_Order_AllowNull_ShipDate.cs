namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Order_AllowNull_ShipDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShipDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ShipDate", c => c.DateTime(nullable: false));
        }
    }
}
