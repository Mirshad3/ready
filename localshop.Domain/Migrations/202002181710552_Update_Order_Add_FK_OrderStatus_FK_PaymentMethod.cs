namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order_Add_FK_OrderStatus_FK_PaymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatusId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "PaymentMethodId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "OrderStatusId");
            CreateIndex("dbo.Orders", "PaymentMethodId");
            AddForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatuses", "Id");
            AddForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatuses");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropColumn("dbo.Orders", "PaymentMethodId");
            DropColumn("dbo.Orders", "OrderStatusId");
        }
    }
}
