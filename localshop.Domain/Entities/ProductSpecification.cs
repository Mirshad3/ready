using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class ProductSpecification
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string ScreenSize { get; set; }

        public string DisplayTechnology { get; set; }

        public string MaxScreenResolution { get; set; }

        public string Processor { get; set; }

        public string RearFrontCamera { get; set; }

        public string ExpandableMemory	 { get; set; }

        public string USBTechnology { get; set; }

        public bool? Fingerprint { get; set; }

        public bool? NFC { get; set; }

        public string RAM { get; set; }

        public string HardDrive { get; set; }

        public string GraphicsCoprocessor { get; set; }

        public string ChipsetBrand { get; set; }

        public string CardDescription { get; set; }

        public string GraphicsCardRamSize { get; set; }

        public string WirelessType { get; set; }

        public int? NumberOfUSB2Dot0Ports { get; set; }

        public int? NumberOfUSB3Dot0Ports { get; set; }

        public string BrandName { get; set; }

        public string Series { get; set; }

        public string ItemModelNumber { get; set; }

        public string HardwarePlatform { get; set; }

        public string OperatingSystem { get; set; }

        public string ItemWeight { get; set; }

        public string ProductDimensions { get; set; }

        public string ItemDimensionsLxWxH { get; set; }

        public string ProcessorBrand { get; set; }

        public int? ProcessorCount { get; set; }

        public string ComputerMemoryType { get; set; }

        public string FlashMemorySize { get; set; }

        public string HardDriveInterface { get; set; }

        public string HardDriveRotationalSpeed { get; set; }

        public string OpticalDriveType { get; set; }

        public string Batteries { get; set; }
    }
}
