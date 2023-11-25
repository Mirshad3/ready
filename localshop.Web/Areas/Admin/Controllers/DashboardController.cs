using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMapper _mapper;
        public DashboardController()
        {
        }

        public DashboardController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IMapper mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _mapper = mapper;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var model = UserManager.FindById(User.Identity.GetUserId());
            if (model.Address1 == null || model.PhoneNumber == null || model.LastName == null)
            {
               return RedirectToAction("UpdateProfile", "Account");
            }
            return View(model);
        }
    }
}