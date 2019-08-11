using System.Web;
using System.Web.Optimization;

namespace IndiaEvents2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/toastr.min.js"));

            bundles.Add(new StyleBundle("~/Content/mdbcss").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/site.css",
                      "~/Content/css/mdb.css",
                      "~/Content/css/mdb.lite.css",
                      "~/Content/css/mdb.lite.min.css",
                      "~/Content/css/mdb.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/style.min.css",
                      "~/Content/font/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/toastr.less",
                      "~/Content/toastr.min.css",
                      "~/Content/toastr.scss"
                      ));

            bundles.Add(new ScriptBundle("~/Content/mdbjquery").Include(
                "~/Content/js/bootstrap.js",
                "~/Content/bootstrap.min.js",
                "~/Content/jquery-3.3.1.min.js",
                "~/Content/mdb.js",
                "~/Content/mdb.min.js",
                "~/Content/popper.min.js"
                ));

            
    

        }
    }
}
