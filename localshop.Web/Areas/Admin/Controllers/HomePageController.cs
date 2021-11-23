using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class HomePageController : BaseController
    {
        private IMapper _mapper;
        private IHomePageRepository _homePageRepo;

        public HomePageController(IMapper mapper, IHomePageRepository homePageRepo)
        {
            _mapper = mapper;
            _homePageRepo = homePageRepo;
        }
        [HttpGet]
        public ViewResult SpecialFeaturedList()
        {
            var model = _homePageRepo.SpecialFeaturedList;

            return View(model);
        }
        [HttpGet]
        public ViewResult SpecialFeatured()
        {
            var model = _homePageRepo.SpecialFeatureds;
            if (model == null)
            {
                model = new SpecialFeaturedDTO();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialFeatured(SpecialFeaturedDTO specialFeaturedDTO)
        {
            if (!ModelState.IsValid ||
                string.IsNullOrWhiteSpace(specialFeaturedDTO.BackgroundImage) ||
                string.IsNullOrWhiteSpace(specialFeaturedDTO.ProductImage))
            {
                TempData["SaveSuccess"] = "false";
                TempData["ErrorMessage"] = "Something went wrong, you have to add background image and product image!";
                return View(specialFeaturedDTO);
            }

            var result = _homePageRepo.SaveSpecialFeatureds(specialFeaturedDTO);

            if (!result)
            {
                TempData["SaveSuccess"] = "false";
                return View(specialFeaturedDTO);
            }

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("SpecialFeatured");
        }

        [HttpGet]
        public ViewResult Banners()
        {
            var model = _homePageRepo.Banners.ToList();
            return View(model);
        }

        [HttpGet]
        public ViewResult AddBanner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBanner(BannerDTO bannerDTO)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(bannerDTO.Image))
            {
                if (string.IsNullOrWhiteSpace(bannerDTO.Image))
                {
                    TempData["ErrorMessage"] = "image";
                }
                return View(bannerDTO);
            }

            var result = _homePageRepo.SaveBanner(bannerDTO);
            if (!result)
            {
                TempData["ErrorMessage"] = "true";
                return View(bannerDTO);
            }

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("banners");
        }

        [HttpGet]
        public ViewResult EditBanner(string id)
        {
            var banner = _homePageRepo.Banners.FirstOrDefault(b => b.Id == id);
            if (banner == null)
            {
                return View("banners");
            }

            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBanner(BannerDTO bannerDTO)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(bannerDTO.Image))
            {
                if (string.IsNullOrWhiteSpace(bannerDTO.Image))
                {
                    TempData["ErrorMessage"] = "image";
                }
                return View(bannerDTO);
            }

            var result = _homePageRepo.SaveBanner(bannerDTO);
            if (!result)
            {
                TempData["ErrorMessage"] = "true";
                return View(bannerDTO);
            }

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("banners");
        }


        [HttpPost]
        public JsonResult DeleteBanner(string bannerId)
        {
            var result = _homePageRepo.DeleteBanner(bannerId);

            if (!result)
            {
                return Json(new
                {
                    success = "failed"
                });
            }

            return Json(new
            {
                success = "success"
            });
        }
    }
}