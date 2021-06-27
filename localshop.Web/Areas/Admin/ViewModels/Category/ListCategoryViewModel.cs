using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListCategoryViewModel
    {
        public CategoryDTO Category { get; set; }
        public int ProductCount { get; set; }
    }
}