using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class SpecialFeatured
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Link { get; set; }

        public string BackgroundImage { get; set; }

        public string ProductImage { get; set; }
    }
}
