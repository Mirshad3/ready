using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class InvoiceReportViewModel
    {
        public OrderDTO Order { get; set; }

        public ProductDTO Product { get; set; }

        public OrderDetailDTO OrderDetail { get; set; }
        public string OrderStatus { get; set; }
    }
    public class InvoiceTotalViewModel
    {
        public string UserId { get; set; }

        public decimal ShippingPrice { get; set; } 
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public int DateRange { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public DateTime? ShipDate { get; set; }
        public int DetuctionPersontage { get; set; }

        public decimal Detuction { get; set; }
        public int Tex { get; set; }
        public decimal ReturnTotal { get; set; }
    }
    public class userDropdown
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string UserRole { get; set; }

    }
    }