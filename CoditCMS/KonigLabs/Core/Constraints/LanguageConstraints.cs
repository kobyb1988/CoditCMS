using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using KonigLabs.Models;

namespace KonigLabs.Core.Constraints
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

                switch (lang)
                {
                    case LocalEntity.RU:
                        return true;
                    case LocalEntity.EN:
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