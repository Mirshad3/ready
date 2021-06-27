using localshop.Areas.Admin.ViewModels;
using localshop.Domain.Abstractions;
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

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
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