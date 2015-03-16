using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Compilation;

namespace MedIn.Web.Areas.Admin
{

    public class PluginAreaBootstrapper
    {
        public static readonly List<Assembly> PluginAssemblies = new List<Assembly>();

        public static List<string> PluginNames()
        {
            return PluginAssemblies.Select(
                pluginAssembly => pluginAssembly.GetName().Name)
                .ToList();
        }

        public static void Init()
        {
            var fullPluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Areas");

            foreach (var file in Directory.EnumerateFiles(fullPluginPath, "*Plugin*.dll"))
                PluginAssemblies.Add(Assembly.LoadFile(file));

            PluginAssemblies.ForEach(BuildManager.AddReferencedAssembly);
        }
    }


    //public class CustomViewEngine : RazorViewEngine
    //{
    //    public CustomViewEngine()
    //    {
    //        MasterLocationFormats = new string[]
    //    {
    //        "~/bin/Views/{1}/{0}.cshtml",
    //        "~/bin/Views/Shared/{0}.cshtml",

    //    };
    //        ViewLocationFormats = new string[]
    //    {
    //        "~/bin/Areas/Admin/Views/{1}/{0}.cshtml",  
    //        "~/bin/Areas/Admin/Views/Shared/{0}.cshtml",
    //    };
    //    }
    //}

    public class PluginRazorViewEngine : RazorViewEngine
    {
        public PluginRazorViewEngine()
        {
            AreaMasterLocationFormats = new[]
        {
            "~/Areas/{2}/Views/{1}/{0}.cshtml",
            "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            "~/Areas/{2}/Views/Shared/{0}.cshtml",
            "~/Areas/{2}/Views/Shared/{0}.vbhtml"
        };

            AreaPartialViewLocationFormats = new[]
        {
            "~/Areas/{2}/Views/{1}/{0}.cshtml",
            "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            "~/Areas/{2}/Views/Shared/{0}.cshtml",
            "~/Areas/{2}/Views/Shared/{0}.vbhtml"
        };

            var areaViewAndPartialViewLocationFormats = new List<string>
        {
            "~/Areas/{2}/Views/{1}/{0}.cshtml",
            "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            "~/Areas/{2}/Views/Shared/{0}.cshtml",
            "~/Areas/{2}/Views/Shared/{0}.vbhtml"
        };

            var partialViewLocationFormats = new List<string>
        {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/Shared/{0}.vbhtml"
        };

            var masterLocationFormats = new List<string>
        {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/Shared/{0}.vbhtml"
        };

            foreach (var plugin in PluginAreaBootstrapper.PluginNames())
            {
                masterLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.cshtml");
                masterLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.vbhtml");
                masterLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/Shared/{1}/{0}.cshtml");
                masterLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/Shared/{1}/{0}.vbhtml");

                partialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.cshtml");
                partialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.vbhtml");
                partialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/Shared/{0}.cshtml");
                partialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/Shared/{0}.vbhtml");

                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.cshtml");
                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Views/{1}/{0}.vbhtml");
                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Areas/{2}/Views/{1}/{0}.cshtml");
                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Areas/{2}/Views/{1}/{0}.vbhtml");
                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Areas/{2}/Views/Shared/{0}.cshtml");
                areaViewAndPartialViewLocationFormats.Add(
                    "~/Areas/" + plugin + "/Areas/{2}/Views/Shared/{0}.vbhtml");
            }

            ViewLocationFormats = partialViewLocationFormats.ToArray();
            MasterLocationFormats = masterLocationFormats.ToArray();
            PartialViewLocationFormats = partialViewLocationFormats.ToArray();
            AreaPartialViewLocationFormats = areaViewAndPartialViewLocationFormats.ToArray();
            AreaViewLocationFormats = areaViewAndPartialViewLocationFormats.ToArray();
        }
    }
    public class AdminAreaRegistration : AreaRegistration
    {
        public readonly string Namespace = "MedIn.Web.Areas.Admin.Controllers";

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
            ViewEngines.Engines.Add(new PluginRazorViewEngine());
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
