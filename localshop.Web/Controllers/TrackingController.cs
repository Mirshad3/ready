using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class TrackingController : Controller
    {
        private IOrderRepository _orderRepo;

        public TrackingController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            var order = _orderRepo.FindById(id);
            List<OrderDetailDTO> orderDetails = null;

            if (order != null)
            {
                orderDetails = _orderRepo.GetOrderDetails(id).ToList();
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
    }
}