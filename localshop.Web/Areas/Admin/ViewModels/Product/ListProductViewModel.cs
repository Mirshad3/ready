using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListProductViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<StatusDTO> Statuses { get; set; }
        public IEnumerable<ApplicationUser> userDetails {get; set; }
    }
}