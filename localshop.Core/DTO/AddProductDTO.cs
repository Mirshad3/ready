using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace localshop.Core.DTO
{
    public class AddProductDTO
    {
        [Required]
        public string Sku { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string ShortDesciption { get; set; }

        [AllowHtml]
        public string LongDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public DateTime? EndDiscountDate { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int? Weight { get; set; }
        public int? Width { get; set; }
        public int? Hight { get; set; }

        [Required]
        public bool IsFeatured { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string StatusId { get; set; }

        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public string Images { get; set; }

        public ProductSpecificationDTO ProductSpecification { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}