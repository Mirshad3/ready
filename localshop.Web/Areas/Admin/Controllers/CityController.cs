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
    public class CityController : BaseController
    {
        private IMapper _mapper;
        private ICityRepository _CityRepo;

        public CityController(IMapper mapper, ICityRepository CityRepo)
        {
            _mapper = mapper;
            _CityRepo = CityRepo;
        }


        public ActionResult Index()
        {
            var cities = _CityRepo.Cities.ToList().Select(c => new ListCityViewModel
            {
                City = c
            });

            var model = new ListAddCityViewModel
            {
                Cities = cities,
                AddCity = new CityDTO()
            };

            return View(model);
             
        }

        [HttpPost]
        public JsonResult Delete(string CityId)
        {
            var City = _CityRepo.Delete(CityId);

            if (City != null)
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
        public ActionResult Add(CityDTO CityDTO)
        {
            var result = _CityRepo.Save(CityDTO);
            if (result)
            {
                TempData["AddSuccess"] = "Success";
                return RedirectToAction("index");
            }

            TempData["AddSuccess"] = "Failed";
            return RedirectToAction("index");
        }

        [HttpGet]
        public JsonResult Edit(string CityId)
        {
            var City = _CityRepo.FindById(CityId);
            if (City == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                City = City,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CityDTO CityDTO)
        {
            var result = _CityRepo.Save(CityDTO);

            if (!result)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(new
            {
                City = CityDTO,
                success = true
            });
        }
    }
}