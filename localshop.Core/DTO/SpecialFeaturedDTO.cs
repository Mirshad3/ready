using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class SpecialFeaturedDTO
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string BackgroundImage { get; set; }

        [Required]
        public string ProductImage { get; set; }
    }
}
