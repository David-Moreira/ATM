using System.Web.Optimization;

namespace ATM
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min",
                      "~/Assets/dist/css/flat-ui.min.css",
                      "~/Content/demo.min.css",
                      "~/Content/main.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Assets/dist/js/flat-ui.min.js",
                      "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalization").Include(
                      "~/Scripts/globalize*",
                      "~/Scripts/cldr*"));
        }
    }
}