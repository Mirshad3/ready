using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Infrastructures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            General();
            AdminProfile();
            ClientProfile();
        }

        public void General()
        {
            #region Domain to DTO
            CreateMap<Review, ReviewDTO>();
            CreateMap<ProductSpecification, ProductSpecificationDTO>();
            #endregion

            #region DTO to Domain
            CreateMap<ReviewDTO, Review>();
            CreateMap<ProductSpecificationDTO, ProductSpecification>();
            #endregion
        }

        public void AdminProfile()
        {
            #region Domain to DTO
            CreateMap<ApplicationUser, UpdateProfileDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<City, CityDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<Status, StatusDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<SpecialFeatured, SpecialFeaturedDTO>();
            CreateMap<Banner, BannerDTO>();
            CreateMap<Contact, ContactDTO>();
            #endregion


            #region DTO to Domain
            CreateMap<UpdateProfileDTO, ApplicationUser>();
            CreateMap<ProductDTO, Product>().ForMember(m => m.Images, opt => opt.Ignore());
            CreateMap<CategoryDTO, Category>();
            CreateMap<CityDTO, City>();
            CreateMap<SpecialFeaturedDTO, SpecialFeatured>();
            CreateMap<BannerDTO, Banner>();
            #endregion


            #region DTO to DTO
            CreateMap<AddProductDTO, ProductDTO>().ForMember(m => m.Images, opt => opt.Ignore());
            CreateMap<EditProductDTO, ProductDTO>().ForMember(m => m.Images, opt => opt.Ignore());
            #endregion
        }

        public void ClientProfile()
        {
            #region Domain to DTO
            CreateMap<ApplicationUser, OrderDTO>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.UserId, opt => opt.MapFrom(u => u.Id));
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<Wishlist, WishlistDTO>();
            #endregion


            #region DTO to Domain
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderDetailDTO, OrderDetail>();
            CreateMap<ContactDTO, Contact>();
            CreateMap<WishlistDTO, Wishlist>();
            #endregion


            #region DTO to DTO
            CreateMap<ProductDTO, OrderDetailDTO>()
                .ForMember(od => od.Id, opt => opt.Ignore())
                .ForMember(od => od.ProductId, opt => opt.MapFrom(p => p.Id));
            #endregion
        }
    }
}