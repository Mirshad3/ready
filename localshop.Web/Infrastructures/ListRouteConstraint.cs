using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace localshop.Infrastructures
{
    public enum ListRouteConstraintType
    {
        Exclude,
        Include
    }

    public class ListRouteConstraint : IRouteConstraint
    {
        public ListRouteConstraintType ListType { get; set; }
        public IList<string> List { get; set; }

        public ListRouteConstraint() : this(ListRouteConstraintType.Include, new string[] { }) { }
        public ListRouteConstraint(params string[] list) : this(ListRouteConstraintType.Include, list) { }
        public ListRouteConstraint(ListRouteConstraintType listType, params string[] list)
        {
            if (list == null) throw new ArgumentNullException("list");

            this.ListType = listType;
            this.List = new List<string>(list);
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (string.IsNullOrEmpty(parameterName)) throw new ArgumentNullException("parameterName");
            if (values == null) throw new ArgumentNullException("values");

            string value = values[parameterName.ToLower()] as string;
            bool found = this.List.Any(s => s.Equals(value, StringComparison.OrdinalIgnoreCase));

            return this.ListType == ListRouteConstraintType.Include ? found : !found;
        }
    }

}