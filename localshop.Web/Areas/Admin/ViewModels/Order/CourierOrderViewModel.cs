using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    [Serializable]
    public class CourierOrderViewModel
    {
         
        public string apikey { get; set; } = "KwyHFZKKZeqrWyDyEhqr";
        public int orderWaybillid { get; set; }
        public string orderNo { get; set; }
        public string receiverName { get; set; }
        public string receiverStreet { get; set; }

        public string receiverDistrict { get; set; }
        public string receiverCity { get; set; }

        public string receiverPhone { get; set; }
        public string description { get; set; }
        public string spclNote { get; set; }

        public string getCod { get; set; }
    }
}