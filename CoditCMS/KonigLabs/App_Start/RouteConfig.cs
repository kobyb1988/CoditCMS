using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using CMS.Mvc;
using KonigLabs.Core;
using KonigLabs.Core.Constraints;

namespace KonigLabs
{
    public static class RouteConfig
    {
        private const string Namespace = "KonigLabs.Controllers";

        private static readonly LanguageConstraints LanguageConstraint = new LanguageConstraints();
        private static readonly LanguageRouteHandler LanguageHandler = new LanguageRouteHandler();

        private static void MapRouteLang(this RouteCollection routes, string name, string url, ActionResult action, string page, string localizationRedirectRouteName = "homepage", object constraints = null, object handler = null)
        {
            var constraintsValues = new RouteValueDictionary(constraints) { { "lang", LanguageConstraint } };
            var route = CreateRoute(name, "{lang}/" + url, action, new { location = page, localizationRedirectRouteName }, constraintsValues, new[] { Namespace });
            var directRoute = CreateRoute(name, url, action, new { location = page, localizationRedirectRouteName }, constraints, new[] { Namespace });
            directRoute.RouteHandler = LanguageHandler;
            routes.Add(name + "-lang", route);
            routes.Add(name, directRoute);
        }

        private static KonigLabsRoute CreateRoute(string routeName, string url, ActionResult result, object defaults, object constraints, string[] namespaces)
        {
            var defaultsValues = new RouteValueDictionary(defaults);
            foreach (var pair in result.GetRouteValueDictionary())
            {
                defaultsValues.Add(pair.Key, pair.Value);
            }
            var constraintsValues = constraints as RouteValueDictionary;

            if (constraintsValues == null)
            {
                constraintsValues = new RouteValueDictionary(constraints);
            }
            var route = new KonigLabsRoute(url, defaultsValues, constraintsValues, new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary()
            };
            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }
            route.DataTokens["RouteName"] = routeName;
            return route;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLang("homepage", "", MVC.Home.Index(), "home");
            routes.MapRouteLang("Comment","Comment",MVC.Home.Comment().Result,"Comment");
            routes.MapRouteLang("Blog","Blog",MVC.Home.Blog(), "Blog");
            routes.MapRouteLang("Article","BlogPost/{id}",MVC.Home.BlogPost(), "Article");
            routes.MapRouteLang("login", "Account/login", MVC.Account.Login(), "");
            routes.MapRouteLang("logout", "Account/logout", MVC.Account.LogOut(), "");
            routes.MapRouteLang("members", "Home/member", MVC.Home.Member(), "");
            routes.MapRouteLang("member", "Home/member/{id}", MVC.Home.Member(), "");            
           // routes.MapRouteLang("GetProjects", "Home/GetProjects/{page}", MVC.Home.GetProjects(), "");
            routes.MapRouteLang("projects", "Home/Projects/{page}", MVC.Home.Projects(), "");
            routes.MapRouteLang("project", "Home/project/{id}", MVC.Home.Project(), "");
            routes.MapRouteLang("contact", "Home/contact", MVC.Home.Contact().Result, "");


            var route = routes.MapRoute("static-pages-lang", "{lang}/{*location}", MVC.Home.Index(),
                new { localizationRedirectRouteName = "homepage" },
                new { lang = LanguageConstraint }, null);
            //route.DataTokens["RouteName"] = "static-pages-lang";

            //route = routes.MapRoute("static-pages", "{*location}", MVC.Home.Index(),
            //    new { localizationRedirectRouteName = "homepage" });
            //route.DataTokens["RouteName"] = "static-pages";

            //routes.MapRoute("default-t4", "{controller}/{action}", MVC.Home.Index());
            //routes.MapRoute("default", "{controller}/{action}/{id}", MVC.Home.Index());
            //routes.MapRoute("default-aliased", "{controller}/{action}/{alias}", MVC.Home.Index());
        }

        
    }
}
