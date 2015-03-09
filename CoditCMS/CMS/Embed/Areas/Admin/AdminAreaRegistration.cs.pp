using System.Web.Mvc;

namespace  $rootnamespace$.Areas.Admin
{
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
            
			            context.MapRoute(
               "Admin_start",
               "Admin",
               new { controller = "SiteSettings", action = "General" },
               new[] { Namespace }
           );

            context.MapRoute(
                "Admin_action",
               "Admin/{controller}",
                new { action = "General" },
                new[] { Namespace }
            );

            context.MapRoute(
                        "Admin_default",
                        "Admin/{controller}/{action}/{id}",
                        new { action = "Index", id = UrlParameter.Optional }
                        );
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
