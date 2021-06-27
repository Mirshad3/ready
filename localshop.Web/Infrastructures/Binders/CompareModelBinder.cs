using localshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Infrastructures
{
    public class CompareModelBinder : IModelBinder
    {
        private string sessionKey = "Compare";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Compare compare = null;

            if (controllerContext.HttpContext.Session != null)
            {
                compare = (Compare)controllerContext.HttpContext.Session[sessionKey];
            }

            if (compare == null)
            {
                compare = new Compare();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = compare;
                }
            }

            return compare;
        }
    }
}