using AutoMapper;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Infrastructures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    [ManageAuthorize]
    public class CategoryController : BaseController
    {
        private IMapper _mapper;
        private ICategoryRepository _categoryRepo;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }


        public ActionResult Index()
        {
            var categories = _categoryRepo.Categories.ToList().Select(c => new ListCategoryViewModel
            {
                Category = c,
                ProductCount = _categoryRepo.CountProduct(c.Id)
            });

            var model = new ListAddCategoryViewModel
            {
                Categories = categories,
                AddCategory = new CategoryDTO()
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(string categoryId)
        {
            var category = _categoryRepo.Delete(categoryId);

            if (category != null)
            {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryDTO categoryDTO)
        {
            var result = _categoryRepo.Save(categoryDTO);
            if (result)
            {
                TempData["AddSuccess"] = "Success";
                return RedirectToAction("index");
            }

            TempData["AddSuccess"] = "Failed";
            return RedirectToAction("index");
        }

        [HttpGet]
        public JsonResult Edit(string categoryId)
        {
            var category = _categoryRepo.FindById(categoryId);
            if (category == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                category = category,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CategoryDTO categoryDTO)
        {
            var result = _categoryRepo.Save(categoryDTO); 
            _categoryRepo.InactiveProduct(categoryDTO.Id, categoryDTO.IsActive);
            
            if (!result)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(new
            {
                category = categoryDTO,
                success = true
            });
        }
    }
}