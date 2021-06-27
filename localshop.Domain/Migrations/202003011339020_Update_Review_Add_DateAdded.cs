namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Review_Add_DateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "DateAdded");
        }
    }
}
