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

		}
	}
}
