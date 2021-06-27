using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ReviewViewModel
    {
        public string Name { get; set; }
        public ProductDTO Product { get; set; }
        public ReviewDTO Review { get; set; }
    }
}