using System.Web;
using System.Web.Optimization;

namespace MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            StyleBundle myCSSBundle = new StyleBundle("~/Content/CustomCSS");
            myCSSBundle.Include("~/Content/sb-admin-2.css", "~/Content/dataTables.bootstrap4.css", "~/Content/PagedList.css");
            bundles.Add(myCSSBundle);

            ScriptBundle mySCBundle = new ScriptBundle("~/bundles/CustomSC");
            mySCBundle.Include("~/Scripts/jquery.easing.js", "~/Scripts/sb-admin-2.js", "~/Scripts/datatables/jquery.dataTables.js", "~/Scripts/datatables/dataTables.bootstrap4.js");
            bundles.Add(mySCBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}