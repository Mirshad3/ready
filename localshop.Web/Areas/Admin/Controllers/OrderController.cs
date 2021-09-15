﻿using localshop.Areas.Admin.ViewModels;
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
    public class OrderController : BaseController
    {
        private IOrderRepository _orderRepo;
        private ApplicationUserManager _userManager;
        public OrderController(ApplicationUserManager userManager, IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
            UserManager = userManager;
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
            var model = new List<OrderViewModel>();

            var orders = _orderRepo.Orders.OrderByDescending(o => o.OrderDate);
            foreach (var o in orders)
            {
                var order = new OrderViewModel
                {
                    Order = o,
                    PaymentMethod = _orderRepo.GetPaymentMethod(o.PaymentMethodId),
                    OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
                };

                model.Add(order);
            }

            return View(model);
        }
        public ActionResult IndexVendor()
        {
            var model = new List<OrderViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid).OrderByDescending(o => o.OrderDate);
            foreach (var o in orders)
            {
                var order = new OrderViewModel
                {
                    Order = o,
                    PaymentMethod = _orderRepo.GetPaymentMethod(o.PaymentMethodId),
                    OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
                };

                model.Add(order);
            }

            return View(model);
        }
        public ActionResult IndexCourier()
        {
            var model = new List<OrderViewModel>();
            var user = UserManager.FindById(User.Identity.GetUserId());
            var orders = _orderRepo.GetOrdersByZone(user.City).OrderByDescending(o => o.OrderDate);
            foreach (var o in orders)
            {
                var order = new OrderViewModel
                {
                    Order = o,
                    PaymentMethod = _orderRepo.GetPaymentMethod(o.PaymentMethodId),
                    OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
                };

                model.Add(order);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetOrderStatus(string orderId)
        {
            var order = _orderRepo.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            var orderStatus = _orderRepo.GetOrderStatus(order.OrderStatusId);
            if (orderStatus == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                orderStatus = orderStatus
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatus(string orderId, string statusName)
        {
            var result = _orderRepo.UpdateStatus(orderId, statusName);
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