using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Models
{
    public class ProductFilter
    {
        public ProductFilter()
        {
            FilteredResult = 0;
            Page = 1;
            View = 20;
            SortBy = SortByEnums.Default;
            PriceFilter = new PriceFilter();
        }

        public int FilteredResult { get; set; }


        // Right filter bar
        public int? Page { get; set; }
        public int? View { get; set; }
        public SortByEnums? SortBy { get; set; }
        public ViewMode? ViewMode { get; set; }


        // Left filter bar
        public string Search { get; set; }
        public string Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public PriceFilter PriceFilter { get; set; }
    }
}