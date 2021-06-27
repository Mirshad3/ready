using localshop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Infrastructures.Attributes
{
    public class ManageAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!(user.IsInRole(RoleNames.Administrator) || user.IsInRole(RoleNames.Root)))
            {
                filterContext.Result = new ViewResult() { ViewName = "AccessDenied" };
            }

            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/admin/account/login");
        }
    }
}