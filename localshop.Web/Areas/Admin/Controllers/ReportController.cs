using localshop.Areas.Admin.ViewModels;
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
    public class ReportController : BaseController
    {
        private IOrderRepository _orderRepo;
        private IProductRepository _productRepo;
        private ApplicationUserManager _userManager;
        public ReportController(ApplicationUserManager userManager, IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            UserManager = userManager;
            _productRepo = productRepo;
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
        // GET: Admin/Report
        public ActionResult Index()
        {
            var model = new List<InvoiceReportViewModel>();

            var products = _orderRepo.GetAllOrderDetails().GroupBy(m => m.ProductId)
     .Select(group => new { ProductId = group.FirstOrDefault().ProductId, Items = group.ToList() })
     .ToList();
            foreach (var pr in products)
            {
                var orders = _orderRepo.Orders.OrderByDescending(o => o.OrderDate);
               
                foreach (var o in orders)
                {
                    var order = new InvoiceReportViewModel
                    {
                        Order = o,
                        Product = _productRepo.FindById(pr.ProductId),
                        OrderDetail = pr.Items.FirstOrDefault(),
                        OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
                    };
                    model.Add(order);
                };
               
            }

            return View(model);
        }
        public ActionResult ReturnCash()
        {
            var model = new List<InvoiceReportViewModel>();

            var products = _orderRepo.GetAllOrderDetails();
            foreach (var pr in products)
            {
                
                    var order = new InvoiceReportViewModel
                    {
                        Order = _orderRepo.FindById(pr.OrderId),
                        Product = _productRepo.FindById(pr.ProductId),
                        OrderDetail = pr 
                    };
                    model.Add(order);
                

            }

            return View(model);
        }
    }
}