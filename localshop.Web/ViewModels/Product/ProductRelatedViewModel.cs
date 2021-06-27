using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.ViewModels
{
    public class ProductRelatedViewModel
    {
        public ProductDTO Product { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
    }
}