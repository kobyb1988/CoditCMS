using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Compilation;

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
            
          //  context.MapRoute(
          //    "Admin_default",
          //    "Admin/{controller}/{action}/{id}",
          //    new { action = "Index", id = UrlParameter.Optional }
          //);            
            // aganzha
            context.MapRoute(
                "AdminDefaultStartPage",
                "admin",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index },
                new[] { Namespace }
            );
            context.MapRoute(
                "AdminDefaultAction",
                "admin/{controller}",
                new { action = MVC.Admin.Locations.ActionNames.Index },
                new[] { Namespace }
            );
            context.MapRoute(
                "AdminDefault",
                "admin/{controller}/{action}/{id}",
                new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index, id = UrlParameter.Optional },
                new[] { Namespace }
            );
        }
    }
}
