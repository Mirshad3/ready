using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class NavController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult TrackingHeader()
        {
            return PartialView("_TrackingHeader");
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            return PartialView("_Header");
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }
    }
}