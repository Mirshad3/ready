using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class ReviewController : Controller
    {
        private IProductRepository _productRepo;
        private IReviewRepository _reviewRepo;

        public ReviewController(IProductRepository productRepo, IReviewRepository reviewRepo)
        {
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ReviewDTO reviewDTO)
        {
            TempData["ActivePanel"] = "review";
            var product = _productRepo.FindById(reviewDTO.ProductId);

            if (product == null)
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                TempData["SaveSuccess"] = "false";
                return RedirectToAction("detail", "product", new { metaTitle = product.MetaTitle });
            }

            reviewDTO.UserId = User.Identity.GetUserId();

            var result = _reviewRepo.Add(reviewDTO);
            if (!result)
            {
                TempData["SaveSuccess"] = "false";
                return RedirectToAction("detail", "product", new { metaTitle = product.MetaTitle });
            }

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("detail", "product", new { metaTitle = product.MetaTitle });
        }


        [HttpPost]
        public JsonResult Delete(string productId)
        {
            var result = _reviewRepo.Delete(User.Identity.GetUserId(), productId);

            return Json(new
            {
                success = result
            });
        }
    }
}