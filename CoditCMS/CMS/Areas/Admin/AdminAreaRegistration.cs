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




    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            MasterLocationFormats = new string[]
        {
            "~/bin/Views/{1}/{0}.cshtml",
            "~/bin/Views/Shared/{0}.cshtml",

        };
            ViewLocationFormats = new string[]
        {
            "~/bin/Areas/Admin/Views/{1}/{0}.cshtml",  
            "~/bin/Areas/Admin/Views/Shared/{0}.cshtml",
        };
        }
    }

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
            //ViewEngines.Engines.Add(new CustomViewEngine());            
            context.MapRoute(
              "Admin_default",
              "Admin/{controller}/{action}/{id}",
              new { action = "Index", id = UrlParameter.Optional }
                //,null
                //,new string[] { "Namespace.Application.Controllers" }
          );            
            // aganzha
            //context.MapRoute(
            //    "AdminDefaultStartPage",
            //    "admin",
            //    new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index },
            //    new[] { Namespace }
            //);
            //context.MapRoute(
            //    "AdminDefaultAction",
            //    "admin/{controller}",
            //    new { action = MVC.Admin.Locations.ActionNames.Index },
            //    new[] { Namespace }
            //);
            //context.MapRoute(
            //    "AdminDefault",
            //    "admin/{controller}/{action}/{id}",
            //    new { controller = MVC.Admin.Locations.Name, action = MVC.Admin.Locations.ActionNames.Index, id = UrlParameter.Optional },
            //    new[] { Namespace }
            //);
        }
    }
}
