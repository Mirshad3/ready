using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.ViewModels
{
    public class HomePageViewModel
    {
        public IList<SpecialFeaturedDTO> SpecialFeatured { get; set; }
        public IEnumerable<BannerDTO> Banners { get; set; }
        public IList<ProductViewModel> Featureds { get; set; }
        public IList<ProductViewModel> OnSales { get; set; }
    }
}