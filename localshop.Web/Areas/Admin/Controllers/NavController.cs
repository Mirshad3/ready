using localshop.Areas.Admin.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class NavController : BaseController
    {
        private ApplicationUserManager _userManager;

        public NavController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [ChildActionOnly]
        public PartialViewResult Topbar(string header)
        {

            var model = new TopbarViewModel
            {
                Header = header,
                User = UserManager.FindById(User.Identity.GetUserId())
            };

            return PartialView("_Topbar", model);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebar()
        {
            var model = new SidebarViewModel
            {
                User = UserManager.FindById(User.Identity.GetUserId()),
                Role = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault()
            };

            return PartialView("_Sidebar", model);
        }

        [ChildActionOnly]
        public PartialViewResult SidebarVendor()
        {
            var model = new SidebarViewModel
            {
                User = UserManager.FindById(User.Identity.GetUserId()),
                Role = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault()
            };

            return PartialView("_SidebarVendor", model);
        }
        [ChildActionOnly]
        public PartialViewResult SidebarCourier()
        {
            var model = new SidebarViewModel
            {
                User = UserManager.FindById(User.Identity.GetUserId()),
                Role = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault()
            };

            return PartialView("_SidebarCourier", model);
        }
        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }
    }
}