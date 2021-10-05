namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Detuction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "DetuctionPersontage", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "Detuction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "Tex", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "ReturnTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ReturnTotal");
            DropColumn("dbo.OrderDetails", "Tex");
            DropColumn("dbo.OrderDetails", "Detuction");
            DropColumn("dbo.OrderDetails", "DetuctionPersontage");
        }
    }
}
