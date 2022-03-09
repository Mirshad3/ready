using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class TrackingController : Controller
    {
        private IReturnCashRepository _returnCashRepo;
        private IOrderRepository _orderRepo;
        private ICityRepository _CityRepo;
        private ApplicationUserManager _userManager;
        public TrackingController(IReturnCashRepository returnCashRepo, IOrderRepository orderRepo, ICityRepository CityRepo)
        {
            _returnCashRepo = returnCashRepo;
            _orderRepo = orderRepo;
            _CityRepo = CityRepo;
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
        [HttpGet]
        public ActionResult Index(string id)
        {
            var order = _orderRepo.FindById(id);
            List<OrderDetailDTO> orderDetails = null;

            if (order != null)
            {
                orderDetails = _orderRepo.GetOrderDetails(id).ToList();
                order.City = _CityRepo.FindById(order.City).Name;
            }

            var model = new TrackingViewModel
            {
                Order = order,
                OrderDetails = orderDetails
            };

            if (model.Order != null)
            {
                model.OrderStatus = _orderRepo.GetOrderStatus(order.OrderStatusId) ?? "...";
                model.PaymentMethod = _orderRepo.GetPaymentMethod(order.PaymentMethodId) ?? "...";
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Return(string id)
        {
            var order = _orderRepo.FindById(id);
            List<OrderDetailDTO> orderDetails = null;

            if (order != null)
            {
                orderDetails = _orderRepo.GetOrderDetails(id).ToList();
                order.City = _CityRepo.FindById(order.City).Name;
            }

            var model = new TrackingViewModel
            {
                Order = order,
                OrderDetails = orderDetails
            };

            if (model.Order != null)
            {
                model.OrderStatus = _orderRepo.GetOrderStatus(order.OrderStatusId) ?? "...";
                model.PaymentMethod = _orderRepo.GetPaymentMethod(order.PaymentMethodId) ?? "...";
            }

            return View(model);
        }
        [HttpPost]
        public JsonResult Save(ReturnCashDTO returnCash)
        {
            var userid = User.Identity.GetUserId();
            returnCash.UserId = userid;
            var result = _returnCashRepo.Save(returnCash);

            if (result == null)
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