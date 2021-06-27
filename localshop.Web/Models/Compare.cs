using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Models
{
    public class CompareLine
    {
        public ProductDTO Product { get; set; }
        public ProductSpecificationDTO ProductSpecification { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
    }

    public class Compare
    {
        public List<CompareLine> LineCollection = new List<CompareLine>();
    }
}