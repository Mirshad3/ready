using localshop.Areas.Admin.ViewModels;
using localshop.Core.Common;
using localshop.Domain.Abstractions;
using localshop.Infrastructures.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace localshop.Areas.Admin.Controllers
{
    [ManageAuthorize]
    public class UserController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IOrderRepository _orderRepo;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IOrderRepository orderRepo)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            _orderRepo = orderRepo;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        [HttpGet]
        public ActionResult Index()
        {
            var listUser = new List<ListUserViewModel>();

            foreach (var user in UserManager.Users.OrderByDescending(u => u.CreatedDate).ToList())
            {
                var roles = UserManager.GetRoles(user.Id);

                if (roles.Any(r => r.Contains(RoleNames.Root)))
                {
                    continue;
                }

                var u = new ListUserViewModel
                {
                    User = user,
                    Roles = roles
                };

                listUser.Add(u);
            }

            var model = new ListUserWithRolesViewModel
            {
                ListUser = listUser,
                Roles = RoleManager.Roles.Where(r => r.Name != RoleNames.Root).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<JsonResult> ChangeRole(string userId, string roleName)
        {
            if (RoleManager.RoleExists(roleName) && roleName != RoleNames.Root)
            {
                var roles = await UserManager.GetRolesAsync(userId);

                var removeRoleResult = await UserManager.RemoveFromRolesAsync(userId, roles.ToArray());
                if (removeRoleResult.Succeeded)
                {
                    var result = await UserManager.AddToRoleAsync(userId, roleName);
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                }
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<JsonResult> Delete(string userId)
        {
            bool isSucceed = false;

            var roles = await UserManager.GetRolesAsync(userId);

            var removeRoleResult = await UserManager.RemoveFromRolesAsync(userId, roles.ToArray());
            if (removeRoleResult.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // Set null foreign key
                    _orderRepo.SetNullDeleteUser(user.Id);

                    var result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        isSucceed = true;
                    }
                    else
                    {
                        isSucceed = false;
                    }
                }
                else
                {
                    isSucceed = false;
                }
            }

            if (isSucceed)
            {
                return Json(new
                {
                    success = true
                });
            }

            return Json(new
            {
                success = false
            });
        }
    }
}