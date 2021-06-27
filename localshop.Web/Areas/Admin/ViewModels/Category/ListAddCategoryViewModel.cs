using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListAddCategoryViewModel
    {
        public IEnumerable<ListCategoryViewModel> Categories { get; set; }
        public CategoryDTO AddCategory { get; set; }
    }
}