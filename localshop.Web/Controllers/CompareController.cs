using localshop.Domain.Abstractions;
using localshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class CompareController : Controller
    {
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private IStatusRepository _statusRepo;

        public CompareController(IProductRepository productRepo, ICategoryRepository categoryRepo, IStatusRepository statusRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _statusRepo = statusRepo;
        }

        public ActionResult Index(Compare compare)
        {
            return View(compare);
        }

        [HttpPost]
        public JsonResult AddToCompare(Compare compare, string productId)
        {
            var product = _productRepo.FindById(productId);

            if (product == null)
            {
                return Json(new
                {
                    success = false
                });
            }

            // Check if exist in compare
            var check = compare.LineCollection.Exists(l => l.Product.Id == productId);
            if (check)
            {
                return Json(new
                {
                    success = true
                });
            }

            if (compare.LineCollection.Count == 4)
            {
                compare.LineCollection.RemoveAt(0);
            }

            product.Images = _productRepo.GetImages(productId).ToList();

            var line = new CompareLine
            {
                Product = product, 
                Category = _categoryRepo.GetCategory(product.CategoryId),
                Status = _statusRepo.GetStatus(product.StatusId)
            };

            compare.LineCollection.Add(line);

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public ActionResult RemoveFromCompare(Compare compare, string productId)
        {
            compare.LineCollection.RemoveAll(l => l.Product.Id == productId);
            return RedirectToAction("index");
        }
    }
}