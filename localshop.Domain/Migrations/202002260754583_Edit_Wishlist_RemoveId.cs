namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Wishlist_RemoveId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Wishlists");
            AddPrimaryKey("dbo.Wishlists", new[] { "UserId", "ProductId" });
            DropColumn("dbo.Wishlists", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlists", "Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Wishlists");
            AddPrimaryKey("dbo.Wishlists", new[] { "Id", "UserId" });
        }
    }
}
