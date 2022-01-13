using AutoMapper;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using localshop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMapper _mapper;
        private IOrderRepository _orderRepo;
        private ICityRepository _cityRepo;
        public AccountController(ICityRepository cityRepo,ApplicationUserManager userManager, ApplicationSignInManager signInManager, IMapper mapper, IOrderRepository orderRepo)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _mapper = mapper;
            _orderRepo = orderRepo;
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

        #region Account
        [HttpGet]
        public ViewResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var name = string.IsNullOrWhiteSpace(user.FullName) ? User.Identity.Name : user.FullName;
            return View(model: name);
        }

        [HttpGet]
        public ViewResult Orders()
        {
            var model = new List<OrderViewModel>();

            var orders = _orderRepo.GetOrders(User.Identity.GetUserId()).OrderByDescending(o => o.OrderDate).ToList();
            foreach (var o in orders)
            {
                var order = new OrderViewModel
                {
                    Order = o,
                    OrderStatus = _orderRepo.GetOrderStatus(o.OrderStatusId)
                };
                model.Add(order);
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult Info()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            user.City = _cityRepo.Cities.Where(m => m.Id == user.City).FirstOrDefault().Name;
            return View(user);
        }

        [HttpGet]
        public ViewResult UpdateInfo()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = _mapper.Map<ApplicationUser, UpdateProfileDTO>(user);
            ViewBag.CityName = new SelectList(_cityRepo.Cities.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInfo(UpdateProfileDTO updateProfileDTO)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (!ModelState.IsValid)
            {
                return View(updateProfileDTO);
            }

            var image = user.Image;
            user = _mapper.Map(updateProfileDTO, user);
            user.Image = image;

            UserManager.Update(user);

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("info");
        }

        [HttpGet]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["SaveSuccess"] = "false";
                TempData["ErrorMessage"] = "Something went wrong!";
                return View(changePasswordViewModel);
            }

            var result = UserManager.ChangePassword(User.Identity.GetUserId(),
                                                    changePasswordViewModel.CurrentPassword,
                                                    changePasswordViewModel.NewPassword);

            if (!result.Succeeded)
            {
                TempData["SaveSuccess"] = "false";
                TempData["ErrorMessage"] = result.Errors.First().ToString();
                return View(changePasswordViewModel);
            }

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("changepassword");
        }
        #endregion

        #region Login - Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };

            ViewBag.ReturnUrl = returnUrl;
            return View("LoginRegister", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            var model = new LoginRegisterViewModel
            {
                Login = loginViewModel,
                Register = new RegisterViewModel()
            };
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View("LoginRegister", model);
            }

            var user = UserManager.FindByEmail(loginViewModel.Email);
            if (user != null && !UserManager.IsEmailConfirmed(user.Id))
            {
                return View("SendMail", model: user.Id);
            }

            var result = await SignInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                // return View("LockedOut");
                // Not implemented
                case SignInStatus.RequiresVerification:
                // return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                // Not implemented
                case SignInStatus.Failure:
                // Not implemented
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    TempData["ErrorMessage"] = "Invalid login attempt.";
                    return View("LoginRegister", model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            TempData["ActivePanel"] = "register";
            return RedirectToAction("login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = registerViewModel
            };
            ViewBag.ReturnUrl = returnUrl;
            TempData["ActivePanel"] = "register";

            if (!ModelState.IsValid)
            {
                return View("LoginRegister", model);
            }

            var user = new ApplicationUser { UserName = registerViewModel.RegisterEmail, Email = registerViewModel.RegisterEmail };
            var result = await UserManager.CreateAsync(user, registerViewModel.RegisterPassword);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, RoleNames.Customer);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("confirmEmail", "account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                var body = MailHelper.CreateConfirmEmailBody(ControllerContext, callbackUrl);
                await UserManager.SendEmailAsync(user.Id, "Confirm your account", body);

                return View("SendMail", model: user.Id);
            }

            AddErrors(result);
            TempData["ErrorMessage"] = result.Errors.First().ToString();
            return View("LoginRegister", model);
        } 

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginVendor(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };

            ViewBag.ReturnUrl = returnUrl;
            return View("LoginVendor", model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterVendor()
        {
            TempData["ActivePanel"] = "register";
            return RedirectToAction("loginVendor");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterVendor(RegisterViewModel registerViewModel, string returnUrl)
        {
            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = registerViewModel
            };
            ViewBag.ReturnUrl = returnUrl;
            TempData["ActivePanel"] = "registerVendor";

            if (!ModelState.IsValid)
            {
                return View("LoginVendor", model);
            }

            var user = new ApplicationUser { UserName = registerViewModel.RegisterEmail, Email = registerViewModel.RegisterEmail };
            var result = await UserManager.CreateAsync(user, registerViewModel.RegisterPassword);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, RoleNames.Customer);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                 
                var callbackUrl = Url.Action("confirmEmail", "account", new { userId = user.Id, code = code, type = "Vendor" }, protocol: Request.Url.Scheme);
                var body = MailHelper.CreateConfirmVendorEmailBody(ControllerContext, callbackUrl);
                await UserManager.SendEmailAsync(user.Id, "Confirm your account", body);

                return View("SendMail", model: user.Id);
            }

            AddErrors(result);
            TempData["ErrorMessage"] = result.Errors.First().ToString();
            return View("LoginRegister", model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code, string type = null)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("index");
            }

            var user = UserManager.FindById(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (type != null)
            {
                var body = "We Send You Approvement to our Admin. Your UserName Is " + user.UserName + " User Id is" + user.Id;
                await UserManager.SendEmailAsync(user.Id, "Confirm your account", body);
            }
            if (result.Succeeded)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                TempData["UpdateInfo"] = "true";
                return RedirectToAction("updateinfo");
            }
            else
            {
                return View("Error");
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ResendConfirmMail(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction("index");
            }

            var user = UserManager.FindById(userId);
            if (user == null)
            {
                return View("Error");
            }

            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("confirmEmail", "account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            var body = MailHelper.CreateConfirmEmailBody(ControllerContext, callbackUrl);
            await UserManager.SendEmailAsync(user.Id, "Confirm your account", body);

            return View("SendMail", model: user.Id);
        }
        #endregion

        #region Reset password
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("resetpassword", "account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                var body = MailHelper.CreateResetPasswordConfirmEmailBody(ControllerContext, callbackUrl);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", body);

                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return RedirectToAction("forgotPassword");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            AddErrors(result);
            TempData["ErrorMessage"] = result.Errors.First().ToString();

            return View();
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "home");
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("index", "home");
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