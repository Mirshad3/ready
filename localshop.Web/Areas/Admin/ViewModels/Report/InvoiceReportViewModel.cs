using localshop.Core.DTO;
using System;
using System.Collections.Generic;
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
}