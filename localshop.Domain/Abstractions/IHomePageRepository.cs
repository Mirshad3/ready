using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IHomePageRepository
    {
        SpecialFeaturedDTO SpecialFeatureds { get; }
        IList<SpecialFeaturedDTO> SpecialFeaturedList { get; }
        IList<BannerDTO> Banners { get; }

        bool SaveSpecialFeatureds(SpecialFeaturedDTO specialFeaturedDTO);

        bool SaveBanner(BannerDTO bannerDTO);

        bool DeleteBanner(string bannerId);
    }
}
