using AutoMapper;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using MassTransit;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class OrderRepository : IOrderRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public OrderRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IList<OrderDTO> Orders
        {
            get
            {
                return _context.Orders.AsEnumerable().Select(o => _mapper.Map<Order, OrderDTO>(o)).ToList();
            }
        }

        public IList<OrderDTO> GetOrders(string userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId).AsEnumerable()
                .Select(o => _mapper.Map<Order, OrderDTO>(o)).ToList();
            return orders;
        }
        public IList<OrderDTO> GetOrdersByOwner(string ownerId)
        {
            var orderDetails = _context.OrderDetails.Where(od => od.Product.UserId == ownerId);
            var orders = new List<OrderDTO>();
            foreach (var orderDetail in orderDetails)
            {
                orders = _context.Orders.Where(o => o.Id == orderDetail.OrderId).AsEnumerable()
                  .Select(o => _mapper.Map<Order, OrderDTO>(o)).ToList();
            }
            
            return orders;
        }
        public IList<OrderDTO> GetOrdersByZone(string cityId)
        {
            var orderByCity = _context.Orders.Where(o => o.City == cityId && o.OrderStatusId == OrderStatusNames.Delivered);
            var orders = new List<OrderDTO>();
            foreach (var order in orderByCity)
            {
                var orderDetails = _context.OrderDetails.Where(od => od.OrderId == order.Id);
               
                foreach (var orderDetail in orderDetails)
                {
                    orders = _context.Orders.Where(o => o.Id == orderDetail.OrderId).AsEnumerable()
                      .Select(o => _mapper.Map<Order, OrderDTO>(o)).ToList();
                }
            }
            return orders;
        }
        
        public OrderDTO FindById(string id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return null;
            }
            return _mapper.Map<Order, OrderDTO>(order);
        }

        public IList<OrderDetailDTO> GetOrderDetails(string id)
        {
            var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id).AsEnumerable()
                                                    .Select(od => _mapper.Map<OrderDetail, OrderDetailDTO>(od)).ToList();
            return orderDetails;
        }
        public IList<OrderDetailDTO> GetAllOrderDetails()
        {
            var orderDetails = _context.OrderDetails.OrderBy(k=>k.OrderId).AsEnumerable()
                                                    .Select(od => _mapper.Map<OrderDetail, OrderDetailDTO>(od)).ToList();
            return orderDetails;
        }
        public string GetOrderStatus(string orderStatusId)
        {
            var orderStatus = _context.OrderStatuses.FirstOrDefault(os => os.Id == orderStatusId);
            if (orderStatus == null)
            {
                return null;
            }

            return orderStatus.Name;
        }

        public string GetPaymentMethod(string paymentMethodId)
        {
            var paymentMethod = _context.PaymentMethods.FirstOrDefault(pm => pm.Id == paymentMethodId);
            if (paymentMethod == null)
            {
                return null;
            }

            return paymentMethod.Name;
        }

        public void SetNullDeleteUser(string userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId);
            foreach (var order in orders)
            {
                order.UserId = null;
            }
            _context.SaveChanges();
        }

        public void SetNullDeleteProduct(string productId)
        {
            var orderDetails = _context.OrderDetails.Where(od => od.ProductId == productId);
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.ProductId = null;
            }
            _context.SaveChanges();
        }

        // For DTO
        public string AddPaymentMethod(OrderDTO orderDTO, string paymentMethod)
        {
            var method = _context.PaymentMethods.FirstOrDefault(pm => pm.Name == paymentMethod);
            if (paymentMethod == null)
            {
                return null;
            }

            orderDTO.PaymentMethodId = method.Id;
            return method.Id;
        }

        // For DTO
        public string UpdateStatus(OrderDTO orderDTO, string statusName)
        {
            var status = _context.OrderStatuses.FirstOrDefault(os => os.Name == statusName);
            if (status == null)
            {
                return null;
            }

            orderDTO.OrderStatusId = status.Id;
            return status.Id;
        }

        public string AddPaymentMethod(string orderId, string paymentMethod)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }

            var method = _context.PaymentMethods.FirstOrDefault(pm => pm.Name == paymentMethod);
            if (paymentMethod == null)
            {
                return null;
            }

            order.PaymentMethodId = method.Id;
            _context.SaveChanges();

            return method.Id;
        }

        public string UpdateStatus(string orderId, string statusName)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }

            var status = _context.OrderStatuses.FirstOrDefault(os => os.Name == statusName);
            if (status == null)
            {
                return null;
            }

            var currentOrderStatus = _context.OrderStatuses.First(o => o.Id == order.OrderStatusId);
            var orderDetails = _context.OrderDetails.Where(od => od.OrderId == order.Id).ToList();

            // Restore quantity of product if order is cancelled
            if (status.Name == OrderStatusNames.Cancelled && currentOrderStatus.Name != OrderStatusNames.Cancelled)
            {
                foreach (var od in orderDetails)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == od.ProductId);
                    if (product != null)
                    {
                        product.Quantity += od.Quantity;
                    }
                }
            }

            // Minus quantity of product if order from cancelled to another status
            if (status.Name != OrderStatusNames.Cancelled && currentOrderStatus.Name == OrderStatusNames.Cancelled)
            {
                foreach (var od in orderDetails)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == od.ProductId);
                    if (product != null)
                    {
                        product.Quantity -= od.Quantity;
                    }
                }
            }

            order.OrderStatusId = status.Id;
            _context.SaveChanges();

            return status.Id;
        }

        public OrderDTO Save(OrderDTO orderDTO, IList<OrderDetailDTO> orderDetailDTOs)
        {
            orderDTO.Id = "#" + string.Join("", NewId.Next().ToString("D").ToUpperInvariant().Split('-'));
            orderDTO.OrderDate = DateTime.Now;
            orderDTO.OrderWaybillid = _context.Orders.OrderByDescending(m => m.OrderWaybillid).FirstOrDefault().OrderWaybillid;
            orderDTO.OrderWaybillid = orderDTO.OrderWaybillid + 1;
            var order = _mapper.Map<OrderDTO, Order>(orderDTO);

            var orderDetails = new List<OrderDetail>();
            foreach (var orderDetailDTO in orderDetailDTOs)
            {
                var orderDetail = _mapper.Map<OrderDetailDTO, OrderDetail>(orderDetailDTO);
                orderDetail.OrderId = order.Id;
                orderDetails.Add(orderDetail);
            }

            // Update quantity
            foreach (var od in orderDetails)
            {
                var product = _context.Products.First(p => p.Id == od.ProductId);
                product.Quantity -= od.Quantity;
            }

            try
            {
                _context.Orders.Add(order);
                _context.OrderDetails.AddRange(orderDetails);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return orderDTO;
        }
        public string OrderHistory(int waybillid)
        {
            var values = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/Orderhistory/users");
            httpWebRequest.ReadWriteTimeout = 100000; //this can cause issues which is why we are manually setting this
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String"); // "Basic 4dfsdfsfs4sf5ssfsdfs=="
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // we want to remove new line characters otherwise it will return an error 
                values = "apikey=KwyHFZKKZeqrWyDyEhqr&waybillid=" + waybillid;
                streamWriter.Write(values);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)httpWebRequest.GetResponse();
                string respStr = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Response : " + respStr); // if you want see the output
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic jsonObject = serializer.Deserialize<dynamic>(respStr);
                //dynamic x = jsonObject["order_history"];
                //var valu = jsonObject["order_history"][0]["status_id"];

                return respStr;

            }
            catch (Exception ex)
            {
                throw ex;
                //process exception here   
            }

        }
    }
}
