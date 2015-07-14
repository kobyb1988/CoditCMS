﻿using System.Web;
using System.Web.Routing;

namespace CMS.Areas.Admin.Mvc.Constraints
{
    public class LanguageConstraints : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest)
            {
                if (string.IsNullOrEmpty(parameterName))
                {
                    parameterName = "lang";
                }
                var val = values[parameterName];
                if (val == null)
                    val = "ru";
                var lang = val.ToString().ToLowerInvariant();
                if ((string) route.DataTokens["area"] == "Admin")
                    return true;
                switch (lang)
                {
                    case "ru":
                        return true;
                    case "en":
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                var val = values[parameterName];
                if ("ru".Equals(val) || val == null || string.IsNullOrEmpty(val.ToString()))
                    return false;
                return true;
            }
        }
    }
}