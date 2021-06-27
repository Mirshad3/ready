namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Wishlist_RenameTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Whishlists", newName: "Wishlists");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Wishlists", newName: "Whishlists");
        }
    }
}
