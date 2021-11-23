using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace localshop.ViewModels
{
    public class ShopViewModel
    {
        public PagingInfo PagingInfo { get; set; }

        public IList<ProductViewModel> Products { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<StatusDTO> Statuses { get; set; }

        public ProductFilter Filter { get; set; }
    }
}