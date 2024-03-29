﻿using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class OrderUserViewModel
    {
        public OrderDTO Order { get; set; }
        public string Owner { get; set; }
        public string PaymentMethod { get; set; }

        public string OrderStatus { get; set; }
    }
}