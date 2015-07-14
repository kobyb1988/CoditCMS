using System;
using System.Web.Routing;

namespace KonigLabs.Core
{
    public class KonigLabsRoute:Route
    {
        public KonigLabsRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
		{
		}

		public KonigLabsRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
		{
		}

		public KonigLabsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler)
		{
		}

        public KonigLabsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
		{
		}

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
			var result = base.GetVirtualPath(requestContext, values);

			if (result != null && !string.IsNullOrEmpty(result.VirtualPath))
            {
                var parts = result.VirtualPath.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                var path = parts[0];
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
                var query = string.Empty;
                if (parts.Length > 1)
                {
                    query = "?" + parts[1];
                }

                result.VirtualPath = path + query;

            }
            return result;
        }
    }
}