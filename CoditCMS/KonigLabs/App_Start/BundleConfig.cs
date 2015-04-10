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
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/Content/static/bundles/js").Include(
                      "~/Content/static/js/*.js"));
            bundles.Add(new StyleBundle("~/Content/static/bundles/css").Include(
                      "~/Content/static/css/*.css"));

        }
    }
}
