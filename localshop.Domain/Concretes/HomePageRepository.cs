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
    public class HomePageRepository : IHomePageRepository
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public HomePageRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public SpecialFeaturedDTO SpecialFeatureds
        {
            get
            {
                return _context.SpecialFeatureds.AsEnumerable().Select(sf => _mapper.Map<SpecialFeatured, SpecialFeaturedDTO>(sf)).FirstOrDefault();
            }
        }
        public IList<SpecialFeaturedDTO> SpecialFeaturedList
        {
            get
            {
                return _context.SpecialFeatureds.AsEnumerable().Select(sf => _mapper.Map<SpecialFeatured, SpecialFeaturedDTO>(sf)).ToList();
            }
        }
        
        public IList<BannerDTO> Banners
        {
            get
            {
                return _context.Banners.AsEnumerable().Select(b => _mapper.Map<Banner, BannerDTO>(b)).ToList();
            }
        }

        public bool SaveSpecialFeatureds(SpecialFeaturedDTO specialFeaturedDTO)
        {
            var specialFeatured = _context.SpecialFeatureds.FirstOrDefault(sf => sf.Id == specialFeaturedDTO.Id);

            if (specialFeatured == null)
            {
                specialFeatured = _mapper.Map<SpecialFeaturedDTO, SpecialFeatured>(specialFeaturedDTO);
                specialFeatured.Id = NewId.Next().ToString();

                _context.SpecialFeatureds.Add(specialFeatured);
                _context.SaveChanges();
            }
            else
            {
                specialFeatured = _mapper.Map(specialFeaturedDTO, specialFeatured);
                _context.SaveChanges();
            }

            return true;
        }

        public bool SaveBanner(BannerDTO bannerDTO)
        {
            var banner = _context.Banners.FirstOrDefault(b => b.Id == bannerDTO.Id);

            if (banner == null)
            {
                bannerDTO.Id = NewId.Next().ToString();
                banner = _mapper.Map<BannerDTO, Banner>(bannerDTO);
                _context.Banners.Add(banner);
            }
            else
            {
                banner = _mapper.Map(bannerDTO, banner);
            }

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBanner(string bannerId)
        {
            var banner = _context.Banners.FirstOrDefault(b => b.Id == bannerId);
            if (banner == null)
            {
                return false;
            }

            _context.Banners.Remove(banner);
            _context.SaveChanges();

            return true;
        }
    }
}
