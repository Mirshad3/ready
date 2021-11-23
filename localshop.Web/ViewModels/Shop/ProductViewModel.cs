using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using localshop.ViewModels.Review;
namespace localshop.ViewModels
{
    public class ProductViewModel
    {
        public ProductDTO Product { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public IList<ReviewDTO> Reviews { get; set; }
    }
}