using CKSource.CKFinder.Connector.Core.Authentication;
using Elmah.ContentSyndication;
using Glimpse.Mvc.Tab;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using localshop.Domain.Migrations;
using localshop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;
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
        private IAccountRepository _accountRepo;
        private ICityRepository _CityRepo;
        public ReportController(IAccountRepository accountRepo,ApplicationUserManager userManager, ApplicationRoleManager roleManager, IOrderRepository orderRepo, IProductRepository productRepo, ICityRepository CityRepo)
        {
            _orderRepo = orderRepo;
            UserManager = userManager;
            _productRepo = productRepo;
            RoleManager = roleManager;
            _accountRepo = accountRepo;
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
            //foreach (var pr in products)
            //{
                var orders = _orderRepo.Orders.OrderByDescending(o => o.OrderDate);

            foreach (var pr in products)
            {
                foreach (var nr in pr.Items)
                {
                    var order = new InvoiceReportViewModel
                    {
                        Order = orders.Where(m => m.Id == nr.OrderId).FirstOrDefault(),
                        Product = _productRepo.FindById(nr.ProductId),
                        OrderDetail = nr,
                        OrderStatus = _orderRepo.GetOrderStatus(orders.Where(m => m.Id == nr.OrderId).FirstOrDefault().OrderStatusId)
                    };
                    model.Add(order);
                }
               
            };
            ////foreach (var o in orders)
            ////{
            ////    var productId = products.Select(m => m.Items.Where(k => k.OrderId == o.Id)).FirstOrDefault();
            ////    foreach (var pr in productId)
            ////    {
            ////        var order = new InvoiceReportViewModel
            ////        {
            ////            Order = o,
            ////            Product = _productRepo.FindById(pr.ProductId),
            ////            OrderDetail = pr,
            ////            OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
            ////        };
            ////        model.Add(order);
            ////    };
            ////}
            //}

            return View(model);
        }
        //public ActionResult ReturnCash()
        //{
        //    var model = new List<InvoiceTotalViewModel>();

        //    // var products = _orderRepo.GetAllOrderDetails();

        //    var orders = _orderRepo.Orders;
        //    DateTime day0 = orders[0].OrderDate.AddDays(-1);
        //    var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-a60e-08d7b48f1d56").GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
        //        .Select(x => new {
        //            x.Key,
        //            Date = day0.AddDays(x.Key * 15 + 1),
        //            Total = x.Sum(y => y.Total),
        //            SubTotal = x.Sum(y => y.SubTotal)
        //        });
        //    foreach (var item in q)
        //    {
        //        //Console.WriteLine($"{item.Date.ToString("yyyy-MM-dd")} {item.Amount}");
        //        var deduction = Convert.ToInt32(ConfigurationManager.AppSettings["Detuction"].ToString());
        //        var tex = Convert.ToInt32(ConfigurationManager.AppSettings["Tex"].ToString());
        //        var order = new InvoiceTotalViewModel
        //        {
        //            Total = item.Total,
        //            DateRange = 15,
        //            StartDate = item.Date,
        //            SubTotal = item.SubTotal,

        //            DetuctionPersontage = deduction,
        //            Detuction = (item.SubTotal * deduction) / 100,
        //            ReturnTotal = item.SubTotal - ((item.SubTotal * deduction) / 100),
        //            Tex = tex
        //        };
        //        model.Add(order);
        //    }


        //    return View(model);
        //}
        public IEnumerable<Tuple<DateTime,DateTime>> DateRange()
        {
            DateTime dateFrom = new DateTime(2021, 10, 1);
            DateTime dateTo = new DateTime(2022, 12, 31);

            List<DateTime> splitDates = new List<DateTime>
                  {
             new DateTime(2023,10,15),
             new DateTime(2023,11,1),
             new DateTime(2023,11,15),
             new DateTime(2023,12,1),
             new DateTime(2023,12,15),
             new DateTime(2024,1,1),
             new DateTime(2024,1,15),
             new DateTime(2024,2,1),
             new DateTime(2024,2,15),
             new DateTime(2024,3,1),
             new DateTime(2024,3,15),
             new DateTime(2024,4,1),
             new DateTime(2024,4,15),
             new DateTime(2024,5,1),
             new DateTime(2024,5,15),
             new DateTime(2024,6,1),
             new DateTime(2024,6,15),
             new DateTime(2024,7,1),
             new DateTime(2024,7,15),
             new DateTime(2024,8,1),
             new DateTime(2024,8,15),
             new DateTime(2024,9,1),
             new DateTime(2024,9,15),
             new DateTime(2024,10,1),
             new DateTime(2024,10,15),
             new DateTime(2024,11,1),
             new DateTime(2024,11,15),
             new DateTime(2024,12,1),
             new DateTime(2024,12,15),
                     };

            var realDates = splitDates
   .Where(d => d > dateFrom && d < dateTo)
   .Concat(new List<DateTime>() { dateFrom.AddDays(-1), dateTo })
   .Select(d => d.Date)
   .Distinct()
   .OrderBy(d => d)
   .ToList();
            var dates = realDates.Zip(realDates.Skip(1), (a, b) => Tuple.Create(a.AddDays(1), b));
            return dates;
        }
        public ActionResult ReturnCash(string id)
        {
            var model = new List<InvoiceTotalViewModel>();
            ////////////////////////
            // var products = _orderRepo.GetAllOrderDetails();

            var dates = DateRange(); 
            var orders = id == null? _orderRepo.Orders: _orderRepo.GetOrdersByOwner(id);
            
            DateTime day0 = orders[0].OrderDate.AddDays(-1); 
            foreach (var da in dates) {
                var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-786e-08d7b48f1d56" && m.OrderDate > da.Item1 && m.OrderDate < da.Item2)
                .GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                .Select(x => new
                {
                    x.Key,
                    Date = da,
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
                    StartDate = da.Item1,
                    EndDate = da.Item2,
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
         
        public ActionResult ReturnCashVender()
        {
            var model = new List<InvoiceTotalViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid);
            DateTime day0 = orders[0].OrderDate.AddDays(-1);
            var dates = DateRange();
            foreach (var da in dates)
            {
                var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-a60e-08d7b48f1d56" && m.OrderDate > da.Item1 && m.OrderDate < da.Item2)
                    .GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                .Select(x => new
                {
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
        [HttpGet]
        public ActionResult ReturnCashVenderByUserId(string id)
        {
            var model = new List<InvoiceTotalViewModel>();

            var orders = _orderRepo.GetOrdersByOwner(id);
            if (orders.Count() > 0) {
            DateTime day0 = orders[0].OrderDate.AddDays(-1);
                var dates = DateRange();
                foreach (var da in dates)
                {
                    var q = orders.Where(m => m.OrderStatusId == "f9d10000-d769-34e6-a60e-08d7b48f1d56" && m.OrderDate > da.Item1 && m.OrderDate < da.Item2).GroupBy(x => ((int)((x.OrderDate.Subtract(day0).TotalDays - 1) / 15)))
                .Select(x => new
                {
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
                            UserId = id,
                            DetuctionPersontage = deduction,
                            Detuction = (item.SubTotal * deduction) / 100,
                            ReturnTotal = item.SubTotal - ((item.SubTotal * deduction) / 100),
                            Tex = tex
                        };
                        model.Add(order);
                    }
                }
            }
        
            return View(model);

        }

        [HttpGet]
        public ActionResult ProductByUserId(string id, DateTime date)
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
        [HttpGet]
        public ActionResult Vendors()
        {
            var listUser = new List<ListUserBankViewModel>();

            foreach (var user in UserManager.Users.OrderByDescending(u => u.CreatedDate).ToList())
            {
                var roles = UserManager.GetRoles(user.Id);

                if (roles.Any(r => r.Contains(RoleNames.Root)))
                {
                    continue;
                }

                var u = new ListUserBankViewModel
                {
                    User = user,
                    Roles = roles,
                    bankAccounts = _accountRepo.FindById(user.Id)
                };

                listUser.Add(u);
            }

            var model = new ListUserWithAccountViewModel
            {
                ListUser = listUser,
                Roles = RoleManager.Roles.Where(r => r.Name == RoleNames.Modifier).ToList()
            };

            return View(model);
        }
        public ActionResult View(string id)
        {

            return View();
        }
        }
}