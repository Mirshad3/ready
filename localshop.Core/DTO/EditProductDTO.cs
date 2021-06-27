using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace localshop.Core.DTO
{
    public class EditProductDTO
    {
        [Required]
        public string Id { get; set; }

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

        [Required]
        public bool IsFeatured { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Images { get; set; }

        public string StatusId { get; set; }

        public string CategoryId { get; set; }

        public ProductSpecificationDTO ProductSpecification { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}