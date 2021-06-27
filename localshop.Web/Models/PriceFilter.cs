using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Models
{
    public class PriceFilter
    {
        public PriceFilter()
        {
            MinPrice = 0;
            MaxPrice = 1;
        }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}