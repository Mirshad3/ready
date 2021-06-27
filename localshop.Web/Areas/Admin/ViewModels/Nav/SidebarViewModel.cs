using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class SidebarViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
    }
}