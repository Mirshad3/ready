namespace localshop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProductSpecification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSpecifications",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 128),
                        ScreenSize = c.String(),
                        DisplayTechnology = c.String(),
                        MaxScreenResolution = c.String(),
                        Processor = c.String(),
                        RearFrontCamera = c.String(),
                        ExpandableMemory = c.String(),
                        USBTechnology = c.String(),
                        Fingerprint = c.Boolean(),
                        NFC = c.Boolean(),
                        RAM = c.String(),
                        HardDrive = c.String(),
                        GraphicsCoprocessor = c.String(),
                        ChipsetBrand = c.String(),
                        CardDescription = c.String(),
                        GraphicsCardRamSize = c.String(),
                        WirelessType = c.String(),
                        NumberOfUSB2Dot0Ports = c.Int(),
                        NumberOfUSB3Dot0Ports = c.Int(),
                        BrandName = c.String(),
                        Series = c.String(),
                        ItemModelNumber = c.String(),
                        HardwarePlatform = c.String(),
                        OperatingSystem = c.String(),
                        ItemWeight = c.String(),
                        ProductDimensions = c.String(),
                        ItemDimensionsLxWxH = c.String(),
                        ProcessorBrand = c.String(),
                        ProcessorCount = c.Int(),
                        ComputerMemoryType = c.String(),
                        FlashMemorySize = c.String(),
                        HardDriveInterface = c.String(),
                        HardDriveRotationalSpeed = c.String(),
                        OpticalDriveType = c.String(),
                        Batteries = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSpecifications", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSpecifications", new[] { "ProductId" });
            DropTable("dbo.ProductSpecifications");
        }
    }
}
