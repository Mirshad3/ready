using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Models
{
    public enum SortByEnums
    {
        Default,
        NameAZ,
        NameZA,
        PriceLowToHigh,
        PriceHightToLow
    }

    public enum ViewMode
    {
        Default,
        List
    }
}