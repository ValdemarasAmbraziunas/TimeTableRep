using System.Web;
using System.Web.Optimization;

namespace TimeTable
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/external/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/external/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/external/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/external/bootstrap.js",
                      "~/Scripts/external/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new Bundle("~/bundles/JSComponents").Include(
                "~/Scripts/CompiledJS/utils.js",
                "~/Scripts/CompiledJS/index.js",
                "~/Scripts/CompiledJS/SwapController.js",
                "~/Scripts/CompiledJS/ConsoleController.js",
                "~/Scripts/external/underscore-min.js"

                ));
        }
    }
}
