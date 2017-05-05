using System.Web;
using System.Web.Optimization;

namespace KonigLabs
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            //bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/Content/media/bundles/js")
                      .Include("~/Content/media/js/21_bootstrap.min.js")
                      .Include("~/Content/media/js/4_smooth-scroll.js")
                      .Include("~/Content/media/js/5_jquery.appear.js")
                      .Include("~/Content/media/js/3_royal_preloader.min.js")
                      .Include("~/Content/media/js/6_parallax.js")
                      .Include("~/Content/media/js/7_wow.js")
                      .Include("~/Content/media/js/9_charts.js")
                      .Include("~/Content/media/js/8_count.js")
                      .Include("~/Content/media/js/91_jquery.cubeportfolio.min.js")
                      .Include("~/Content/media/js/93_gmap3.min.js")
                      .Include("~/Content/media/js/92_main.js")
                      .Include("~/Content/media/js/94_scripts.js")
                      .Include("~/Content/media/js/95_owl.carousel.min.js")
                      .Include("~/Content/media/js/921_toastr.js")
                      .Include("~/Content/media/js/custom.modernizr.js")
                      .Include("~/Content/media/js/eye.js")
                      .Include("~/Content/media/js/jquery.jkey-1.1.js")
                      .Include("~/Content/media/js/96_YTPlayer.js")
                      .Include("~/Scripts/jquery.validate.min").
                        Include("~/Scripts/jquery.validate.unobtrusive.min")
                      );
            bundles.Add(new ScriptBundle("~/Content/media.mobile/bundles/js")
                           .Include("~/Content/media.mobile/js/bootstrap.min.js")
                      .Include("~/Content/media.mobile/js/jquery.appear.js")
                      .Include("~/Content/media.mobile/js/royal_preloader.min.js")
                      .Include("~/Content/media.mobile/js/wow.js")
                      .Include("~/Content/media.mobile/js/charts.js")
                      .Include("~/Content/media.mobile/js/count.js")
                      .Include("~/Content/media.mobile/js/jquery.cubeportfolio.min.js")
                      .Include("~/Content/media.mobile/js/main.js")
                      .Include("~/Content/media.mobile/js/scripts.js")
                      .Include("~/Content/media.mobile/js/toastr.js")
                      .Include("~/Content/media.mobile/js/custom.modernizr.js")
                      );

            bundles.Add(new StyleBundle("~/Content/media/bundles/css")
                      .Include("~/Content/media/css/1_bootstrap.min.css")
                      .Include("~/Content/media/css/2_style.css")
                      .Include("~/Content/media/css/3_colour.css")
                      .Include("~/Content/media/css/4_animate.css")
                      .Include("~/Content/media/css/5_font-awesome.min.css")
                      .Include("~/Content/media/css/6_owl.carousel.css")
                      .Include("~/Content/media/css/7_cubeportfolio.min.css")
                      );
            bundles.Add(new StyleBundle("~/Content/media.mobile/bundles/css")
                .Include("~/Content/media.mobile/css/bootstrap.min.css")
                .Include("~/Content/media.mobile/css/style.css")
                .Include("~/Content/media.mobile/css/colour.css")
                .Include("~/Content/media.mobile/css/animate.css")
                .Include("~/Content/media.mobile/css/font-awesome.min.css")
                .Include("~/Content/media.mobile/css/cubeportfolio.min.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate.min").
            //            Include("~/Scripts/jquery.validate.unobtrusive.min"));
        }
    }
}
