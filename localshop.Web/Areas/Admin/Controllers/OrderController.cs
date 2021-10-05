using Glimpse.AspNet.Tab;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
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
                var orderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId);
                if (orderStatus == OrderStatusNames.ReadyToShip) {

                    var statusId = _orderRepo.OrderHistory(o.OrderWaybillid);
                    if (statusId != "{\"order_history\":[]}") { 
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic jsonObject = serializer.Deserialize<dynamic>(statusId);
                    dynamic x = jsonObject["order_history"];
                    var valu = jsonObject["order_history"][0]["status_id"];
                    if (valu == "1")
                    {
                        orderStatus = OrderStatusNames.ReadyToShip;
                    }
                    else if (valu == "2" || valu == "3")
                    {
                        orderStatus = OrderStatusNames.Picked;
                    }else
                    {
                        orderStatus = OrderStatusNames.Delivered;
                    }
                       _orderRepo.UpdateStatus(o.Id, orderStatus);
                    }
                }
                var order = new OrderViewModel
                {
                    Order = o,
                    PaymentMethod = _orderRepo.GetPaymentMethod(o.PaymentMethodId),
                    OrderStatus = orderStatus
                };

                model.Add(order);
            }

            return View(model);
        }
        public ActionResult IndexVendor()
        {
            var model = new List<OrderViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid).Where(m=>m.OrderStatusId == "f9d10000-d769-34e6-4e67-08d7b48f1d56").OrderByDescending(o => o.OrderDate);
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
        public ActionResult IndexVendorApproved()
        {
            var model = new List<OrderViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid).Where(m => m.OrderStatusId == "f9d10000-d769-34e6-d603-08d7b94c7194").OrderByDescending(o => o.OrderDate);
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
        public ActionResult IndexVendorReady()
        {
            var model = new List<OrderViewModel>();
            var userid = User.Identity.GetUserId();
            var orders = _orderRepo.GetOrdersByOwner(userid).Where(m => m.OrderStatusId == "f9d10000-d769-34e6-786e-08d7b48f1d56").OrderByDescending(o => o.OrderDate);
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
            if (statusName == "ReadyToShip")
            {
                var oredrdetails = _orderRepo.FindById(orderId);
                var datas = new CourierOrderViewModel {
                orderNo = orderId,
                receiverCity = oredrdetails.City,
                receiverDistrict = oredrdetails.State,
                receiverName = oredrdetails.LastName,
                receiverPhone = oredrdetails.PhoneNumber,
                receiverStreet = oredrdetails.Address1,
                description = "Cash On Delivery",
                spclNote = oredrdetails.OrderNotes == null ? "No Notes": oredrdetails.OrderNotes,
                orderWaybillid = oredrdetails.OrderWaybillid,
                getCod = oredrdetails.Total.ToString()

                };
                var response = PostNewOrderDataToApi(datas);
            }
            return Json(new
            {
                success = true
            });
        }
        public static OrderViewModel getOrderHistory(string waybillid)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Orderhistory/users");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Basic reallylongstring");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                OrderViewModel answer = JsonConvert.DeserializeObject<OrderViewModel>(streamReader.ReadToEnd());
                return answer;
            }
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

        public string PostPickupDataToApi(string jsonData)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Pickups/users");
            httpWebRequest.ReadWriteTimeout = 100000; //this can cause issues which is why we are manually setting this
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String"); // "Basic 4dfsdfsfs4sf5ssfsdfs=="
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // we want to remove new line characters otherwise it will return an error 
                jsonData = "apikey=KwyHFZKKZeqrWyDyEhqr&vehicleType=Bike&pickup_remark=test&pickup_address=sri lanka&latitude=6.901608599999999&longitude=80.0087746&phone=0760961206&qty=10";
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)httpWebRequest.GetResponse();
                string respStr = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Response : " + respStr); // if you want see the output
                return respStr;
            }
            catch (Exception ex)
            {
                throw ex;
                //process exception here   
            }

        }

        public string OrderHistory(string waybillid)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Orderhistory/users");
            httpWebRequest.ReadWriteTimeout = 100000; //this can cause issues which is why we are manually setting this
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String"); // "Basic 4dfsdfsfs4sf5ssfsdfs=="
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // we want to remove new line characters otherwise it will return an error 
                waybillid = "apikey=KwyHFZKKZeqrWyDyEhqr&waybillid=" + waybillid;
                streamWriter.Write(waybillid);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)httpWebRequest.GetResponse();
                string respStr = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Response : " + respStr); // if you want see the output
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic jsonObject = serializer.Deserialize<dynamic>(respStr);
                dynamic x = jsonObject["order_history"];  
                var valu = jsonObject["order_history"][0]["status_id"];   
                 
                return valu;

            }
            catch (Exception ex)
            {
                throw ex;
                //process exception here   
            }

        }
    }
}

//{
//    "apikey": "KwyHFZKKZeqrWyDyEhqr",
//  "vehicleType": "Bike",
//   "pickup_remark": "test",
//   "pickup_address": "sri lanka",
//  "latitude": 6.901608599999999,
//  "longitude": 80.0087746,
//   "qty": 10
//}
