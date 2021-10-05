using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }

        public string Id { get; set; }
        public int OrderWaybillid { get; set; }
        
        public decimal ShippingPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string OrderNotes { get; set; }

        // Order infomation
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }


        // Ship to diffrent address
        public bool IsShipToDifferentAddress { get; set; }

        public string DiffCountry { get; set; }

        public string DiffCity { get; set; }

        public string DiffState { get; set; }

        public string DiffZip { get; set; }

        public string DiffAddress1 { get; set; }

        public string DiffAddress2 { get; set; }

        // Product orders
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        // FK_OrderStatus
        public string OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        // FK_PaymentMethod
        public string PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        // FK_Coupon
        //public string CouponId { get; set; }

        // FK_User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
