using localshop.Domain.Abstractions;
using localshop.ViewModels;
using localshop.ViewModels.Review;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationUserManager _userManager;
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private IStatusRepository _statusRepo;
        private IReviewRepository _reviewRepo;

        public ProductController(ApplicationUserManager userManager, IProductRepository productRepo, ICategoryRepository categoryRepo, IStatusRepository statusRepo, IReviewRepository reviewRepo)
        {
            UserManager = userManager;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _statusRepo = statusRepo;
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

        public ActionResult Detail(string metaTitle)
        {
            var product = _productRepo.FindByMetaTitle(metaTitle);
            if (product == null)
            {
                return RedirectToAction("NotFound");
            }

            var model = new ProductDetailViewModel
            {
                Product = product,
                Status = _statusRepo.GetStatus(product.StatusId),
                Category = _categoryRepo.GetCategory(product.CategoryId), 
                Reviews = new List<ReviewViewModel>(),
                Related = new List<ProductRelatedViewModel>()
            };

            // Get images
            product.Images = _productRepo.GetImages(product.Id).ToList();

            // Get reviews
            var reviews = _reviewRepo.GetReviews(product.Id).OrderByDescending(r => r.DateAdded).ToList();
            var users = UserManager.Users.AsEnumerable().Where(u => reviews.Any(r => r.UserId == u.Id)).ToList();
            foreach (var review in reviews)
            {
                var user = users.First(u => u.Id == review.UserId);
                var reviewViewModel = new ReviewViewModel
                {
                    Name = user.FullName,
                    Review = review
                };
                model.Reviews.Add(reviewViewModel);
            }

            // Get related
            var relatedProduct = _productRepo.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id).ToList();
            foreach (var p in relatedProduct)
            {
                var related = new ProductRelatedViewModel
                {
                    Product = p,
                    Status = _statusRepo.GetStatus(p.StatusId),
                    Category = model.Category
                };
                p.Images = _productRepo.GetImages(p.Id).ToList();
                model.Related.Add(related);
            }

            return View(model);
        }
    }
}