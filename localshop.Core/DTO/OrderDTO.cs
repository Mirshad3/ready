using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public int OrderWaybillid { get; set; }
        public string UserId { get; set; } 

        public decimal ShippingPrice { get; set; }

        [Required]
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? ShipDate { get; set; }

        [Display(Name = "Order notes")]
        public string OrderNotes { get; set; }

        //[Required]
        public string PaymentMethodId { get; set; }

        //[Required]
        public string OrderStatusId { get; set; }

        // Order infomation
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Town / City")]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postcode / ZIP")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Street address")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }


        // Ship to diffrent address
        [Required]
        public bool IsShipToDifferentAddress { get; set; }

        [Display(Name = "Country")]
        public string DiffCountry { get; set; }

        [Display(Name = "City")]
        public string DiffCity { get; set; }

        [Display(Name = "State")]
        public string DiffState { get; set; }

        [Display(Name = "Zip")]
        public string DiffZip { get; set; }

        [Display(Name = "Street address")]
        public string DiffAddress1 { get; set; }

        public string DiffAddress2 { get; set; }
    }
}
