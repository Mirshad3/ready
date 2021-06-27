using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class TopbarViewModel
    {
        public string Header { get; set; }
        public ApplicationUser User { get; set; }
    }
}