using AutoMapper;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private IMapper _mapper;
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private IStatusRepository _statusRepo;
        private IOrderRepository _orderRepo;

        public ProductController(IMapper mapper,
                                IProductRepository productRepo,
                                ICategoryRepository categoryRepo,
                                IStatusRepository statusRepo,
                                IOrderRepository orderRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _statusRepo = statusRepo;
            _orderRepo = orderRepo;
        }

        public ViewResult Index()
        {
            var products = _productRepo.AllProducts.OrderByDescending(p => p.DateAdded).ToList();
            foreach (var p in products)
            {
                p.Images = _productRepo.GetImages(p.Id).ToList();
            }

            var model = new ListProductViewModel
            {
                Products = products,
                Categories = _categoryRepo.Categories,
                Statuses = _statusRepo.Statuses
            };
            return View(model);
        }

        public ViewResult IndexVendor()
        {
            var products = _productRepo.AllProducts.Where(m=>m.UserId == User.Identity.GetUserId()).OrderByDescending(p => p.DateAdded).ToList();
            foreach (var p in products)
            {
                p.Images = _productRepo.GetImages(p.Id).ToList();
            }

            var model = new ListProductViewModel
            {
                Products = products,
                Categories = _categoryRepo.Categories,
                Statuses = _statusRepo.Statuses
            };
            return View(model);
        }
        [HttpGet]
        public ViewResult Add()
        {
            var model = new ProductViewModel
            {
                Product = new ProductDTO(),
                ProductSpecification = new ProductSpecificationDTO(),
                Categories = _categoryRepo.Categories.AsEnumerable(),
                Statuses = _statusRepo.Statuses.AsEnumerable()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                var model = new ProductViewModel
                {
                    Product = new ProductDTO(),
                    ProductSpecification = new ProductSpecificationDTO(),
                    Categories = _categoryRepo.Categories.AsEnumerable(),
                    Statuses = _statusRepo.Statuses.AsEnumerable(),
                    IsFeatured = productDTO.IsFeatured,
                    IsActive = productDTO.IsActive,
                };
                return View(model);
            }

            if (_productRepo.FindBySku(productDTO.Sku) != null)
            {
                var model = new ProductViewModel
                {
                    Product = _mapper.Map<AddProductDTO, ProductDTO>(productDTO),
                    ProductSpecification = productDTO.ProductSpecification,
                    Categories = _categoryRepo.Categories.AsEnumerable(),
                    Statuses = _statusRepo.Statuses.AsEnumerable(),
                    IsFeatured = productDTO.IsFeatured,
                    IsActive = productDTO.IsActive
                };
                ModelState.AddModelError("", "SKU is used by another product.");
                return View(model);
            }
            productDTO.UserId = User.Identity.GetUserId();
            var images = new List<string>();
            if (!string.IsNullOrWhiteSpace(productDTO.Images))
            {
                foreach (var imgUrl in productDTO.Images.Split('@'))
                {
                    images.Add(imgUrl);
                }
            }

            var product = _mapper.Map<AddProductDTO, ProductDTO>(productDTO);
            product.Images = images;

            _productRepo.Save(product, productDTO.ProductSpecification);

            TempData["SaveSuccess"] = "Success";
            return RedirectToAction("add");
        }

        [HttpPost]
        public JsonResult Delete(string productId)
        {
            var product = _productRepo.FindById(productId);
            if (product != null)
            {
                _orderRepo.SetNullDeleteProduct(productId);
                _productRepo.Delete(productId);

                return Json(new
                {
                    success = true
                });
            }

            return Json(new
            {
                success = false
            });
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var product = _productRepo.FindById(id);

            if (product != null)
            {
                product.Images = _productRepo.GetImages(id).ToList();

                var model = new ProductViewModel
                {
                    Product = product,
                    //ProductSpecification = _productRepo.GetProductSpecification(product.Id),
                    IsFeatured = product.IsFeatured,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId,
                    Categories = _categoryRepo.Categories.AsEnumerable(),
                    StatusId = product.StatusId,
                    Statuses = _statusRepo.Statuses.AsEnumerable(),
                    Images = string.Join("@", product.Images.ToArray())
                };

                return View(model);
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductDTO editProductDTO)
        {
            var product = _productRepo.FindById(editProductDTO.Id);

            var errorModel = new ProductViewModel
            {
                Product = product ?? _mapper.Map<EditProductDTO, ProductDTO>(editProductDTO),
                //ProductSpecification = editProductDTO.ProductSpecification,
                CategoryId = editProductDTO.CategoryId,
                Categories = _categoryRepo.Categories.AsEnumerable(),
                StatusId = editProductDTO.StatusId,
                Statuses = _statusRepo.Statuses.AsEnumerable(),
                Images = editProductDTO.Images,
                IsFeatured = editProductDTO.IsFeatured,
                IsActive = editProductDTO.IsActive
            };

            if (!ModelState.IsValid)
            {
                return View(errorModel);
            }

            if (product != null)
            {
                if (editProductDTO.Sku != product.Sku && _productRepo.FindBySku(editProductDTO.Sku) != null)
                {
                    ModelState.AddModelError("", "SKU is used by another product.");
                    return View(errorModel);
                }

                var productEdited = _mapper.Map(editProductDTO, product);

                productEdited.Images = new List<string>();
                if (!string.IsNullOrWhiteSpace(editProductDTO.Images))
                {
                    var newImages = editProductDTO.Images.Split('@');
                    foreach (var img in newImages)
                    {
                        productEdited.Images.Add(img);
                    }
                }

                _productRepo.Save(productEdited, editProductDTO.ProductSpecification);

                TempData["SaveSuccess"] = "Success";
                return RedirectToAction("index");
            }

            ModelState.AddModelError("", "Product Id is invalid.");
            return View(errorModel);
        }
    }
}