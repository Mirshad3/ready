using localshop.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace localshop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Detail",
                url: "product/{action}/{metaTitle}",
                defaults: new { controller = "product", action = "detail" },
                constraints: new { metaTitle = new MinLengthRouteConstraint(1) },
                namespaces: new[] { "localshop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
                constraints: new { controller = new ListRouteConstraint(ListRouteConstraintType.Exclude, "product") },
                namespaces: new[] { "localshop.Controllers" }
            );
        }
    }
}
