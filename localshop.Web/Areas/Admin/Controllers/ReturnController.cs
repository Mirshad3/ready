using CKSource.CKFinder.Connector.Core.Authentication;
using Glimpse.AspNet.Tab;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.PeerToPeer;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

 
namespace localshop.Areas.Admin.Controllers
{
    public class ReturnController : BaseController
    {
        private IReturnCashRepository _returnCashRepo;
        private IOrderRepository _orderRepo;
        private ApplicationUserManager _userManager;
        private IProductRepository _productRepo;
        private ICityRepository _cityRepo;
        public ReturnController(ApplicationUserManager userManager, IReturnCashRepository returnCashRepo,  IOrderRepository orderRepo, IProductRepository productRepo, ICityRepository cityRepo)
        {
            _returnCashRepo = returnCashRepo;
            _orderRepo = orderRepo;
            UserManager = userManager;
            _productRepo = productRepo;
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
        public ActionResult Index()
        {
            var model = new List<ReturnCashDTO>();
            var returnlist = _returnCashRepo.ReturnCashs;
            foreach (var o in returnlist)
            {
                o.ProductId = _productRepo.Products.FirstOrDefault(p => p.Id == o.ProductId).MetaTitle;
                model.Add(o);
            }
                return View(model);
        }
        public ActionResult IndexVendor()
        {
            var model = new List<OrderViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid).Where(m => m.OrderStatusId == "f9d10000-d769-34e6-4e67-08d7b48f1d56").OrderByDescending(o => o.OrderDate);
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
        public JsonResult GetReturnStatus(string orderId)
        {
            var order = _returnCashRepo.ReturnCashs.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
            

            return Json(new
            {
                success = true,
                orderStatus = order.Status
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(ReturnCashDTO returnCash)
        {
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
        [HttpPost]
        public JsonResult UpdateStatus(string orderId, string statusName)
        {
            var result = _returnCashRepo.UpdateStatus(orderId, statusName);
            if (result == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            if (statusName == "ReadyToShip")
            {
                var returndetails = _returnCashRepo.FindById(orderId);
                var oredrdetails = _orderRepo.FindById(orderId);
                var details = _orderRepo.GetOrderDetails(orderId);
                ////var datas = new CourierOrderViewModel
                ////{
                ////    orderNo = orderId,
                ////    receiverCity = oredrdetails.City,
                ////    receiverDistrict = oredrdetails.State,
                ////    receiverName = oredrdetails.LastName,
                ////    receiverPhone = oredrdetails.PhoneNumber,
                ////    receiverStreet = oredrdetails.Address1,
                ////    description = "Cash On Delivery",
                ////    spclNote = oredrdetails.OrderNotes == null ? "No Notes" : oredrdetails.OrderNotes,
                ////    orderWaybillid = oredrdetails.OrderWaybillid,
                ////    getCod = oredrdetails.Total.ToString()

                ////};
                var product = _productRepo.FindById(details.FirstOrDefault().ProductId);
                var user = UserManager.FindById(product.UserId);
                if (user.Address1.Count() > 0 || user.Address2.Count() > 0)
                {
                    var datas = new CourierOrderViewModel
                    {
                        orderNo = orderId,
                        receiverCity = user.City,
                        receiverDistrict = user.State,
                        receiverName = user.LastName,
                        receiverPhone = user.PhoneNumber,
                        receiverStreet = user.Address1,
                        description = "Return Cash",
                        spclNote = returndetails.OtherReason,
                        orderWaybillid = 87787879,
                        getCod = returndetails.Total.ToString()

                    };

                    var response = PostNewOrderDataToApi(datas);
                    var city = _cityRepo.Cities.Where(m => m.Id == user.City).FirstOrDefault().Name;
                    var track = PostPickupDataToApi(oredrdetails.FirstName + oredrdetails.LastName, oredrdetails.Address1, oredrdetails.Address2, city, oredrdetails.State, details.Sum(m => m.Quantity), 989898989, oredrdetails.PhoneNumber);
                }
            }
            return Json(new
            {
                success = true
            });
        }
    
        public string PostNewOrderDataToApi(CourierOrderViewModel courierOrderViewModel)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Addorders/users");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String");
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = new JavaScriptSerializer().Serialize(courierOrderViewModel);
                StringBuilder postData = new StringBuilder();
                postData.Append("apikey=" + HttpUtility.UrlEncode(courierOrderViewModel.apikey) + "&");
                postData.Append("orderWaybillid=" + HttpUtility.UrlEncode(courierOrderViewModel.orderWaybillid.ToString()) + "&");
                postData.Append("orderNo=" + HttpUtility.UrlEncode(courierOrderViewModel.orderNo) + "&");
                postData.Append("receiverName=" + HttpUtility.UrlEncode(courierOrderViewModel.receiverName) + "&");
                postData.Append("receiverStreet=" + HttpUtility.UrlEncode(courierOrderViewModel.receiverStreet) + "&");
                postData.Append("receiverDistrict=" + HttpUtility.UrlEncode(courierOrderViewModel.receiverDistrict) + "&");
                postData.Append("receiverCity=" + HttpUtility.UrlEncode(courierOrderViewModel.receiverCity) + "&");
                postData.Append("receiverPhone=" + HttpUtility.UrlEncode(courierOrderViewModel.receiverPhone) + "&");
                postData.Append("description=" + HttpUtility.UrlEncode(courierOrderViewModel.description) + "&");
                postData.Append("spclNote=" + HttpUtility.UrlEncode(courierOrderViewModel.spclNote) + "&");
                postData.Append("getCod=" + HttpUtility.UrlEncode(courierOrderViewModel.getCod));
                //jsonData = "apikey=KwyHFZKKZeqrWyDyEhqr&orderWaybillid=14373285&orderNo=123&receiverName=kamal&receiverStreet=no123bibile&receiverDistrict=1&receiverCity=1&receiverPhone=0776030666&description=testorder&spclNote=remark&getCod=1000";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string requestResult = null;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                requestResult = streamReader.ReadToEnd();
            }
            return requestResult;
        }
        public string PostPickupDataToApi(string FullName, string Address1, string Address2, string City, string State, int Quantity, int OrderWaybillid, string Number)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Pickups/users");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String");
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                StringBuilder postData = new StringBuilder();
                postData.Append("apikey=KwyHFZKKZeqrWyDyEhqr&vehicleType=Bike&");
                postData.Append("pickup_remark=" + HttpUtility.UrlEncode(OrderWaybillid.ToString() + " " + FullName) + "&");
                postData.Append("pickup_address=" + HttpUtility.UrlEncode(Address1 + " " + Address2 + " " + City + " " + State) + "&latitude=7.901608599999999&longitude=88.0087746&");
                postData.Append("phone=" + HttpUtility.UrlEncode(Number) + "&");
                postData.Append("qty=" + HttpUtility.UrlEncode(Quantity.ToString()));
                //jsonData =  "apikey=KwyHFZKKZeqrWyDyEhqr&vehicleType=Bike&pickup_remark=test&pickup_address=sri lanka&latitude=6.901608599999999&longitude=80.0087746&phone=0760961206&qty=10";
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string requestResult = null;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                requestResult = streamReader.ReadToEnd();
            }
            return requestResult;
        }

        
    }
}