using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Compilation;
using CMS.Areas.Admin.Mvc.Constraints;

namespace CMS.Areas.Admin
{


    public class AdminAreaRegistration : AreaRegistration
    {
        public readonly string Namespace = "CMS.Areas.Admin.Controllers";

        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
          ////  context.MapRoute(
          ////    "Admin_default",  
          ////    "Admin/{controller}/{action}/{id}",
          ////    new { action = "Index", id = UrlParameter.Optional }
          ////);            
          //  // aganzha
          //  context.MapRoute(
          //      "AdminDefaultStartPage",
          //      "admin",
          //      new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index },
          //      new[] { Namespace }
          //  );
          //  context.MapRoute(
          //      "AdminDefaultAction",
          //      "admin/{controller}",
          //      new { action = MVC.Admin.Locations.ActionNames.Index },
          //      new[] { Namespace }
          //  );
          //  context.MapRoute(
          //      "AdminDefault",
          //      "admin/{controller}/{action}/{id}",
          //      new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index, id = UrlParameter.Optional },
          //      new[] { Namespace }
          //  );
            var route = context.MapRoute(
                "AdminDefaultStartPage-lang",
                "{lang}/admin",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index },
                new { lang = new LanguageConstraints() },
                new[] { Namespace }
            );
            route.DataTokens["RouteName"] = "AdminDefaultStartPage-lang";
            route = context.MapRoute(
                "AdminDefaultAction-lang",
                "{lang}/admin/{controller}",
                new { action = MVC.Admin.Locations.ActionNames.Index },
                new { lang = new LanguageConstraints() },
                new[] { Namespace }
            );
            route.DataTokens["RouteName"] = "AdminDefaultAction-lang";
            route = context.MapRoute(
                "AdminDefault-lang",
                "{lang}/admin/{controller}/{action}/{id}",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index, id = UrlParameter.Optional },
                new { lang = new LanguageConstraints() },
                new[] { Namespace }
            );
            route.DataTokens["RouteName"] = "AdminDefault-lang";



            var handler = new LanguageRouteHandler();

            route = context.MapRoute(
                "AdminDefaultStartPage",
                "admin",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index },
                new[] { Namespace }
            );
            route.RouteHandler = handler;
            route.DataTokens["RouteName"] = "AdminDefaultStartPage";
            route = context.MapRoute(
                "AdminDefaultAction",
                "admin/{controller}",
                new { action = MVC.Admin.Locations.ActionNames.Index },
                new[] { Namespace }
            );
            route.RouteHandler = handler;
            route.DataTokens["RouteName"] = "AdminDefaultAction";
            route = context.MapRoute(
                "AdminDefault",
                "admin/{controller}/{action}/{id}",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index, id = UrlParameter.Optional },
                new[] { Namespace }
            );
            route.RouteHandler = handler;
            route.DataTokens["RouteName"] = "AdminDefault";
        }
    }
}
