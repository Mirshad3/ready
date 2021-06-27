using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class WishlistRepository : IWislistRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public WishlistRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IList<WishlistDTO> GetWishlists(string userId)
        {
            return _context.Wishlists.Where(w => w.UserId == userId).AsEnumerable().Select(w => _mapper.Map<Wishlist, WishlistDTO>(w)).ToList();
        }

        public bool CheckExist(WishlistDTO wishlistDTO)
        {
            return _context.Wishlists.FirstOrDefault(w => w.UserId == wishlistDTO.UserId && w.ProductId == wishlistDTO.ProductId) != null;
        }

        public bool AddToWishlist(WishlistDTO wishlistDTO)
        {
            if (CheckExist(wishlistDTO))
            {
                return true;
            }

            var wishlist = _mapper.Map<WishlistDTO, Wishlist>(wishlistDTO);

            try
            {
                _context.Wishlists.Add(wishlist);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveFromWishlist(WishlistDTO wishlistDTO)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(w => w.UserId == wishlistDTO.UserId && w.ProductId == wishlistDTO.ProductId);
            if (wishlist == null)
            {
                return false;
            }

            try
            {
                _context.Wishlists.Remove(wishlist);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
