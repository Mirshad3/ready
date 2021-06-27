using System.Web.Mvc;

namespace localshop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { action = "index", controller = "dashboard", id = UrlParameter.Optional },
                namespaces: new[] { "localshop.Areas.Admin.Controllers" }
            );
        }
    }
}