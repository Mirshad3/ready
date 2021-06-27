using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListUserWithRolesViewModel
    {
        public IList<ListUserViewModel> ListUser { get; set; }
        public IList<ApplicationRole> Roles { get; set; }
    }
}