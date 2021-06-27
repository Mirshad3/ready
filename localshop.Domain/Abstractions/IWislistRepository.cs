using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IWislistRepository
    {
        IList<WishlistDTO> GetWishlists(string userId);
        bool CheckExist(WishlistDTO whishlistDTO);
        bool AddToWishlist(WishlistDTO whishlistDTO);
        bool RemoveFromWishlist(WishlistDTO wishlistDTO);
    }
}
