using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public OrderDTO Order { get; set; }

        public string PaymentMethod { get; set; }

        public string OrderStatus { get; set; }
    }
    public class OrderViewModelWithUser
    {
        public OrderDTO Order { get; set; }

        public string PaymentMethod { get; set; }

        public string OrderStatus { get; set; }
        public ApplicationUser User { get; set; } 
    }

    
}