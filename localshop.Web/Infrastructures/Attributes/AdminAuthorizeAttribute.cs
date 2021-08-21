using localshop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Infrastructures.Attributes
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var user = filterContext.HttpContext.User;
                if (!(user.IsInRole(RoleNames.Root) ||
                      user.IsInRole(RoleNames.Administrator) || user.IsInRole(RoleNames.Courier) ||
                      user.IsInRole(RoleNames.Modifier)))
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }

            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/admin/account/login");
        }
    }
}