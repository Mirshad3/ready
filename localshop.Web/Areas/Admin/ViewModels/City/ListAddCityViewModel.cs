using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListAddCityViewModel
    {
        public IEnumerable<ListCityViewModel> Cities { get; set; }
        public CityDTO AddCity { get; set; }
    }
}