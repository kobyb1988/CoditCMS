using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KonigLabs.Core
{
    public class LanguageRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] == null)
            {
                requestContext.RouteData.Values["lang"] = "ru";
            }

            if ("ru".Equals(requestContext.RouteData.Values["lang"]))
            {
                requestContext.RouteData.Values["lang"] = null;
            }

            return base.GetHttpHandler(requestContext);
        }
    }
}