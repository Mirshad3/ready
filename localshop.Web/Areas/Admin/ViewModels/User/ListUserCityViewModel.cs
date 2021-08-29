using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListUserCityViewModel
    {
        public UpdateProfileDTO User { get; set; }
        public IEnumerable<CityDTO> City { get; set; }
    }
}