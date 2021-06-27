using localshop.Areas.Admin.ViewModels;
using localshop.Domain.Abstractions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class ReviewController : BaseController
    {
        private ApplicationUserManager _userManager;
        private IProductRepository _productRepo;
        private IReviewRepository _reviewRepo;

        public ReviewController(ApplicationUserManager userManager, IProductRepository productRepo, IReviewRepository reviewRepo)
        {
            UserManager = userManager;
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var model = new List<ReviewViewModel>();

            var reviews = _reviewRepo.Reviews.Where(r => r.IsApproved).OrderByDescending(r => r.DateAdded).ToList();
            foreach (var review in reviews)
            {
                var user = UserManager.FindById(review.UserId);
                var product = _productRepo.FindById(review.ProductId);

                var reviewViewModel = new ReviewViewModel
                {
                    Name = string.IsNullOrWhiteSpace(user.FullName) ? "(No name)" : user.FullName,
                    Product = product,
                    Review = review
                };

                model.Add(reviewViewModel);
            }

            return View(model);
        }

        public ActionResult Waiting()
        {
            var model = new List<ReviewViewModel>();

            var reviews = _reviewRepo.Reviews.Where(r => !r.IsApproved).OrderByDescending(r => r.DateAdded).ToList();
            foreach (var review in reviews)
            {
                var user = UserManager.FindById(review.UserId);
                var product = _productRepo.FindById(review.ProductId);

                var reviewViewModel = new ReviewViewModel
                {
                    Name = string.IsNullOrWhiteSpace(user.FullName) ? "(No name)" : user.FullName,
                    Product = product,
                    Review = review
                };

                model.Add(reviewViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetReview(string userId, string productId)
        {
            var review = _reviewRepo.Reviews.FirstOrDefault(r => r.UserId == userId && r.ProductId == productId);
            if (review == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            var user = UserManager.FindById(review.UserId);
            var product = _productRepo.FindById(review.ProductId);

            var reviewViewModel = new ReviewViewModel
            {
                Name = string.IsNullOrWhiteSpace(user.FullName) ? "(No name)" : user.FullName,
                Product = product,
                Review = review
            };

            return Json(new
            {
                success = true,
                model = reviewViewModel
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Approve(string userId, string productId)
        {
            var result = _reviewRepo.Approve(userId, productId);

            return Json(new
            {
                success = result
            });
        }

        [HttpPost]
        public JsonResult Delete(string userId, string productId)
        {
            var result = _reviewRepo.Delete(userId, productId);

            return Json(new
            {
                success = result
            });
        }
    }
}