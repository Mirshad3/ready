using localshop.Core.Common;
using localshop.Infrastructures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace localshop.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class BaseController : Controller
    {
    }
}