using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class ProductSpecificationDTO
    {
        public string ProductId { get; set; }

        [Display(Name = "Screen size")]
        public string ScreenSize { get; set; }

        [Display(Name = "Display technology")]
        public string DisplayTechnology { get; set; }

        [Display(Name = "Max screen resolution")]
        public string MaxScreenResolution { get; set; }

        public string Processor { get; set; }

        [Display(Name = "Rear/Front camera")]
        public string RearFrontCamera { get; set; }

        [Display(Name = "Expandable memory")]
        public string ExpandableMemory { get; set; }

        [Display(Name = "USB technology")]
        public string USBTechnology { get; set; }

        public bool? Fingerprint { get; set; }

        public bool? NFC { get; set; }

        public string RAM { get; set; }

        [Display(Name = "Hard drive")]
        public string HardDrive { get; set; }

        [Display(Name = "Graphics coprocessor")]
        public string GraphicsCoprocessor { get; set; }

        [Display(Name = "Chipset brand")]
        public string ChipsetBrand { get; set; }

        [Display(Name = "Card description")]
        public string CardDescription { get; set; }

        [Display(Name = "Graphics card ram size")]
        public string GraphicsCardRamSize { get; set; }

        [Display(Name = "Wireless type")]
        public string WirelessType { get; set; }

        [Display(Name = "Number of USB 2.0 ports")]
        public int? NumberOfUSB2Dot0Ports { get; set; }

        [Display(Name = "Number of USB 3.0 ports")]
        public int? NumberOfUSB3Dot0Ports { get; set; }

        [Display(Name = "Brand name")]
        public string BrandName { get; set; }

        public string Series { get; set; }

        [Display(Name = "Item model number")]
        public string ItemModelNumber { get; set; }

        [Display(Name = "Hardware platform")]
        public string HardwarePlatform { get; set; }

        [Display(Name = "Operating system")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Item weight")]
        public string ItemWeight { get; set; }

        [Display(Name = "Product dimensions")]
        public string ProductDimensions { get; set; }

        [Display(Name = "Item dimensions L x W x H")]
        public string ItemDimensionsLxWxH { get; set; }

        [Display(Name = "Processor brand")]
        public string ProcessorBrand { get; set; }

        [Display(Name = "Processor count")]
        public int? ProcessorCount { get; set; }

        [Display(Name = "Computer memory type")]
        public string ComputerMemoryType { get; set; }

        [Display(Name = "Flash memory size")]
        public string FlashMemorySize { get; set; }

        [Display(Name = "Hard drive interface")]
        public string HardDriveInterface { get; set; }

        [Display(Name = "Hard drive rotational speed")]
        public string HardDriveRotationalSpeed { get; set; }

        [Display(Name = "Optical drive type")]
        public string OpticalDriveType { get; set; }

        public string Batteries { get; set; }
    }
}
