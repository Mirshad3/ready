using AutoMapper;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using localshop.Models;
using localshop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class CheckoutController : Controller
    {
        private IMapper _mapper;
        private ApplicationUserManager _userManager;
        private IProductRepository _productRepo;
        private IOrderRepository _orderRepo;
        private ICityRepository _cityRepo;
        public CheckoutController(ICityRepository cityRepo, IMapper mapper, ApplicationUserManager userManager, IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _cityRepo = cityRepo;
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
        public ActionResult Index(Cart cart)
        {
            if (cart.LineCollection.Count == 0)
            {
                TempData["EmptyMessage"] = "true";
                return RedirectToAction("index", "cart");
            }

            var model = new CheckoutViewModel();

            // If user logged in => Get address infomation
            if (Request.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                model.Order = _mapper.Map<ApplicationUser, OrderDTO>(user);
            }
            else
            {
                model.Order = new OrderDTO();
            }

            // Get order details from cart
            model.OrderDetails = GetOrderDetails(cart);
            ViewBag.CityName = new SelectList(_cityRepo.Cities.ToList(), "Id", "Name");
            // Calculate summary
            model.Order.SubTotal = cart.Summary;

            return View(model);
        }

        [HttpGet]
        public decimal GetShippingCost(string CityId)
        {

            var model = new CheckoutViewModel();
            if (Request.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                model.Order = _mapper.Map<ApplicationUser, OrderDTO>(user);
            }
            else
            {
                model.Order = new OrderDTO();
            }
            var costs = _cityRepo.FindById(CityId);
            model.Order.ShippingPrice = costs.Amount;
            model.Order.Total = model.Order.SubTotal + model.Order.ShippingPrice;
            return costs.Amount;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PlaceOrder(Cart cart, OrderDTO order, string paymentMethod)
        {
            if (cart.LineCollection.Count == 0)
            {
                TempData["EmptyMessage"] = "true";
                return RedirectToAction("index", "cart");
            }

            // Add paymentMethod
            var addMethodResult = _orderRepo.AddPaymentMethod(order, paymentMethod);

            // Add orderStatus
            var addOrderStatusResult = _orderRepo.UpdateStatus(order, OrderStatusNames.Pending);

            // Check model valid
            if (!ModelState.IsValid || addMethodResult == null || addOrderStatusResult == null)
            {
                var model = new CheckoutViewModel()
                {
                    Order = order,
                    OrderDetails = GetOrderDetails(cart)
                };
                return View("index", model);
            }

            // Add userId if authenticated
            if (User.Identity.IsAuthenticated)
            {
                order.UserId = User.Identity.GetUserId();
            }

            // Get order details and calulate summary
            var orderDetails = GetOrderDetails(cart, false);
            order.SubTotal = cart.Summary;
            order.ShippingPrice = GetShippingCost(order.City);
            order.Total = order.SubTotal + order.ShippingPrice;

            // Save order to repository
            var result = _orderRepo.Save(order, orderDetails);

            if (result == null)
            {
                TempData["OrderSuccess"] = "false";
                return RedirectToAction("index", "cart");
            }

            // Send mail
            var callbackUrl = Url.Action("index", "tracking", new { id = result.Id }, protocol: Request.Url.Scheme);
            var body  =MailHelper.CreateSuccessPlacingOrderEmailBody(ControllerContext, callbackUrl, result.Id);
            var message = new IdentityMessage
            {
                Subject = $"Order {result.Id}",
                Destination = result.Email,
                Body = body
            };
            var emailService = new EmailService();
            await emailService.SendAsync(message);

            TempData["OrderSuccess"] = "true";
            cart.LineCollection.Clear();

            return RedirectToAction("index", "tracking", new { id = result.Id });
        }

        private List<OrderDetailDTO> GetOrderDetails(Cart cart, bool turnOnNotification = true)
        {
            var orderDetails = new List<OrderDetailDTO>();

            foreach (var line in cart.LineCollection)
            {
                var orderDetail = _mapper.Map<ProductDTO, OrderDetailDTO>(line.Product);
                var product = _productRepo.FindById(line.Product.Id);
                orderDetail.Price = _productRepo.GetRealPrice(product);

                if (line.Quantity > product.Quantity)
                {
                    if (turnOnNotification)
                    {
                        TempData["OutOfStock"] = "true";
                    }
                    line.Quantity = product.Quantity;
                }

                orderDetail.Quantity = line.Quantity;
                orderDetail.SubTotal = orderDetail.Price * orderDetail.Quantity;
                var deduction = Convert.ToInt32(ConfigurationManager.AppSettings["Detuction"].ToString());
                orderDetail.DetuctionPersontage = deduction;
                orderDetail.Detuction = (orderDetail.SubTotal * orderDetail.DetuctionPersontage) / 100;
                orderDetail.ReturnTotal = orderDetail.SubTotal - orderDetail.Detuction;
                orderDetail.Tex = Convert.ToInt32(ConfigurationManager.AppSettings["Tex"].ToString());
                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }

    }
}