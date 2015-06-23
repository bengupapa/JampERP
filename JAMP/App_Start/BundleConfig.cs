using System.Web;
using System.Web.Optimization;

namespace JAMP
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            #endregion

            #region Jquery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            #endregion

            #region Modernizr
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            #endregion

            #region CSS Bundles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/toastr/toastr.min.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/moment-datepicker/datePicker-bootstrap.css",
                        "~/Content/moment-datepicker/datePicker.css",
                        "~/Content/Jamp_app.css",
                        "~/Content/tester.css"));
            #endregion

            #region Foundation Bundles
            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                      "~/Scripts/foundation/foundation.js",
                      "~/Scripts/foundation/foundation.*",
                      "~/Scripts/foundation/app.js"));
            #endregion

            #region JAMP JavaScript Header
            bundles.Add(new ScriptBundle("~/bundles/JAMP_head").Include(
                        "~/Scripts/spin/spin.js"));
            #endregion

            #region JAMP JavaScript Body
            bundles.Add(new ScriptBundle("~/bundles/JAMP_body").Include(
                        "~/Scripts/slide_menu/slide.js",
                        "~/Scripts/app_setup/app.js",
                        "~/Scripts/toastr/toastr.js",
                         "~/Scripts/moment.min.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/sammy-{version}.js",
                        "~/Scripts/q.js",
                        "~/Scripts/breeze.debug.js",
                        "~/Scripts/app_setup/app.js",
                        "~/Scripts/jquery.signalR-{version}.js",
                        "~/Scripts/highcharts.js",
                        "~/Scripts/highcharts-more.js",
                        "~/Scripts/moment-datepicker/moment-datepicker.min.js",
                        "~/Scripts/moment-datepicker/moment-datepicker-ko.js"));
            #endregion
        }
    }
}