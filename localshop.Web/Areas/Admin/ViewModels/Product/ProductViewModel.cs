using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            IsActive = true;
            IsFeatured = false;
        }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; }

        public ProductDTO Product { get; set; }

        public ProductSpecificationDTO ProductSpecification { get; set; }

        public string Images { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }

        [Display(Name = "Status")]
        public string StatusId { get; set; }
        public IEnumerable<StatusDTO> Statuses { get; set; }
    }
}