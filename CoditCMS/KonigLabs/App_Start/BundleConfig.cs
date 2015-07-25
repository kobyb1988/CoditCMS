using System.Web;
using System.Web.Optimization;

namespace KonigLabs
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            //bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/Content/media/bundles/js").Include(
                      "~/Content/media/js/*.js"));
            bundles.Add(new ScriptBundle("~/Content/media.mobile/bundles/js").Include(
                      "~/Content/media.mobile/js/*.js"));

            bundles.Add(new StyleBundle("~/Content/media/bundles/css").Include(
                      "~/Content/media/css/*.css"));
            bundles.Add(new StyleBundle("~/Content/media.mobile/bundles/css").Include(
                      "~/Content/media.mobile/css/*.css"));
        }
    }
}
