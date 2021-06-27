using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private IWislistRepository _wishlistRepo;
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private IStatusRepository _statusRepo;

        public WishlistController(IWislistRepository wishlistRepo, IProductRepository productRepo, ICategoryRepository categoryRepo, IStatusRepository statusRepo)
        {
            _wishlistRepo = wishlistRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _statusRepo = statusRepo;
        }

        public ActionResult Index()
        {
            var wishlists = _wishlistRepo.GetWishlists(User.Identity.GetUserId()).ToList();

            var model = new List<ProductViewModel>();
            foreach (var p in wishlists)
            {
                var product = _productRepo.FindById(p.ProductId);
                product.Images = _productRepo.GetImages(product.Id).ToList();

                var productViewModel = new ProductViewModel
                {
                    Product = product,
                    Status = _statusRepo.GetStatus(product.StatusId),
                    Category = _categoryRepo.GetCategory(product.CategoryId)
                };

                model.Add(productViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult CheckExist(string productId)
        {
            var wishlistDTO = new WishlistDTO
            {
                UserId = User.Identity.GetUserId(),
                ProductId = productId
            };

            if (!_wishlistRepo.CheckExist(wishlistDTO))
            {
                return Json(new
                {
                    exist = false
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                exist = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddToWishlist(string productId)
        {
            var wishlistDTO = new WishlistDTO
            {
                UserId = User.Identity.GetUserId(),
                ProductId = productId
            };

            var result = _wishlistRepo.AddToWishlist(wishlistDTO);
            if (!result)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public JsonResult RemoveFromWishlist(string productId)
        {
            var wishlistDTO = new WishlistDTO
            {
                UserId = User.Identity.GetUserId(),
                ProductId = productId
            };

            var result = _wishlistRepo.RemoveFromWishlist(wishlistDTO);
            if (!result)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(new
            {
                success = true
            });
        }
    }
}