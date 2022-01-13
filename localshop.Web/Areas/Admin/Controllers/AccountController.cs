using AutoMapper;
using CKSource.CKFinder.Connector.Core.Authentication;
using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using localshop.Infrastructures.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMapper _mapper;
        private ICityRepository _cityRepo;
        public AccountController()
        {
        }

        public AccountController(ICityRepository cityRepo,ApplicationUserManager userManager, ApplicationSignInManager signInManager, IMapper mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _mapper = mapper;
            _cityRepo = cityRepo;
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

        [HttpGet]
        public new ViewResult Profile()
        {
            var model = UserManager.FindById(User.Identity.GetUserId());
            model.City = _cityRepo.Cities.Where(m => m.Id == model.City).FirstOrDefault().Name;
            return View(model);
        }

        [HttpGet]
        public ViewResult UpdateProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userDto = _mapper.Map<ApplicationUser, UpdateProfileDTO>(user);
            userDto.Image += $"?t={DateTime.Now.Ticks}";
            ViewBag.CityName = new SelectList(_cityRepo.Cities.ToList(), "Id", "Name");
            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UpdateProfileDTO userDto, HttpPostedFileBase image)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (!ModelState.IsValid)
            {
                userDto.Image = user.Image;
                return View(userDto);
            }

            if (image != null)
            {
                userDto.Image = Path.Combine(@"\Assets\images\useravatars\", $"user_{user.Id}" + Path.GetExtension(image.FileName));
                string path = Path.Combine(Server.MapPath(@"~\Assets\images\useravatars\"), $"user_{user.Id}" + Path.GetExtension(image.FileName));
                image.SaveAs(path);
            }
            else
            {
                userDto.Image = user.Image;
            }

            user = _mapper.Map(userDto, user);

            UserManager.Update(user);

            return RedirectToAction("profile");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated && (
                User.IsInRole(RoleNames.Root) ||
                User.IsInRole(RoleNames.Administrator) ||
                User.IsInRole(RoleNames.Modifier)
                ))
            {
                return RedirectToLocal(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email);
            if (user != null && !UserManager.IsEmailConfirmed(user.Id))
            {
                return RedirectToAction("login", "account", new { area="", returnUrl = returnUrl });
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                //return View("LockedOut");
                // Not implemented
                case SignInStatus.RequiresVerification:
                //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                // Not implemented
                case SignInStatus.Failure:
                // Not implemented
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "dashboard");
        }


        #region Helpers
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("index", "dashboard");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}