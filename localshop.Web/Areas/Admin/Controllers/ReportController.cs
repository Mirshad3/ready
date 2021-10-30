using Elmah.ContentSyndication;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using localshop.Domain.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static System.Net.Mime.MediaTypeNames;

namespace localshop.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private IOrderRepository _orderRepo;
        private IProductRepository _productRepo;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public ReportController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            UserManager = userManager;
            _productRepo = productRepo;
            RoleManager = roleManager;
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
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
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
            var model = new List<InvoiceTotalViewModel>();

           // var products = _orderRepo.GetAllOrderDetails();
            
                var orders = _orderRepo.Orders;
                DateTime day0 = orders[0].OrderDate.AddDays(-1);
                var q = orders.Where(m=>m.OrderStatusId == "f9d10000-d769-34e6-786e-08d7b48f1d56").GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                    .Select(x => new {
                        x.Key,
                        Date = day0.AddDays(x.Key * 15 + 1),
                        Total = x.Sum(y => y.Total),
                        SubTotal = x.Sum(y => y.SubTotal) 
                    });
                foreach (var item in q)
                {
                //Console.WriteLine($"{item.Date.ToString("yyyy-MM-dd")} {item.Amount}");
                var deduction = Convert.ToInt32(ConfigurationManager.AppSettings["Detuction"].ToString());
                var tex = Convert.ToInt32(ConfigurationManager.AppSettings["Tex"].ToString());
                var order = new InvoiceTotalViewModel
                {
                        Total = item.Total,
                        DateRange = 15,
                        StartDate = item.Date,
                        SubTotal = item.SubTotal,

                    DetuctionPersontage = deduction,
                    Detuction = (item.SubTotal * deduction) / 100,
                 ReturnTotal = item.SubTotal - ((item.SubTotal * deduction) / 100),
                Tex = tex
            };
                    model.Add(order);
            }


            return View(model);
        }

        public ActionResult ReturnCashVender()
        {
            var model = new List<InvoiceTotalViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid);
            DateTime day0 = orders[0].OrderDate.AddDays(-1);
            var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-786e-08d7b48f1d56").GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                .Select(x => new {
                    x.Key,
                    Date = day0.AddDays(x.Key * 15 + 1),
                    Total = x.Sum(y => y.Total),
                    SubTotal = x.Sum(y => y.SubTotal)
                });
            foreach (var item in q)
            {
                //Console.WriteLine($"{item.Date.ToString("yyyy-MM-dd")} {item.Amount}");
                var deduction = Convert.ToInt32(ConfigurationManager.AppSettings["Detuction"].ToString());
                var tex = Convert.ToInt32(ConfigurationManager.AppSettings["Tex"].ToString());
                var order = new InvoiceTotalViewModel
                {
                    Total = item.Total,
                    DateRange = 15,
                    StartDate = item.Date,
                    SubTotal = item.SubTotal,
                    
                    DetuctionPersontage = deduction,
                    Detuction = (item.SubTotal * deduction) / 100,
                    ReturnTotal = item.SubTotal - ((item.SubTotal * deduction) / 100),
                    Tex = tex
                };
                model.Add(order);
            }


            return View(model);
        }

        public ActionResult ReturnCashVenderByUserId(string userId)
        {
            var model = new List<InvoiceTotalViewModel>();

            var orders = _orderRepo.GetOrdersByOwner(userId);
            if (orders.Count() > 0) {
            DateTime day0 = orders[0].OrderDate.AddDays(-1);
            var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-a60e-08d7b48f1d56").GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                .Select(x => new {
                    x.Key,
                    Date = day0.AddDays(x.Key * 15 + 1),
                    Total = x.Sum(y => y.Total),
                    SubTotal = x.Sum(y => y.SubTotal)
                });
            foreach (var item in q)
            {
                //Console.WriteLine($"{item.Date.ToString("yyyy-MM-dd")} {item.Amount}");
                var deduction = Convert.ToInt32(ConfigurationManager.AppSettings["Detuction"].ToString());
                var tex = Convert.ToInt32(ConfigurationManager.AppSettings["Tex"].ToString());
                var order = new InvoiceTotalViewModel
                {
                    Total = item.Total,
                    DateRange = 15,
                    StartDate = item.Date,
                    SubTotal = item.SubTotal,

                    DetuctionPersontage = deduction,
                    Detuction = (item.SubTotal * deduction) / 100,
                    ReturnTotal = item.SubTotal - ((item.SubTotal * deduction) / 100),
                    Tex = tex
                };
                model.Add(order);
            }
            }

            return View(model);
        }

        public JsonResult getVendors()
        {
            //holds list of suppliers
            List<userDropdown> _supplierList = new List<userDropdown>();

          

            //save list of suppliers to the _supplierList
            foreach (var user in UserManager.Users.OrderByDescending(u => u.CreatedDate).ToList())
            {
                var roles = UserManager.GetRoles(user.Id);
                _supplierList.Add(new userDropdown
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRole = roles.FirstOrDefault()
                });
            }
          
            //returns the Json result of _supplierList
            return Json(_supplierList.Where(r => r.UserRole == RoleNames.Modifier).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        //static IEnumerable<List<T>> Partition<T>(List<T> input, Func<T, DateTime> DateSelector, TimeSpan partitionSize)
        //{
        //    var partitionEnd = DateSelector(input.First()).Add(partitionSize);
        //    var partition = new List<T>();

        //    foreach (var element in input)
        //    {
        //        var dt = DateSelector(element);
        //        if (dt >= partitionEnd)
        //        {
        //            yield return partition;
        //            partition = new List<T>();
        //            partitionEnd = dt.Add(partitionSize);
        //        }
        //        partition.Add(element);
        //    }
        //    yield return partition;
        //}
    }
}