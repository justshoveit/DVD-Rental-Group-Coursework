﻿using System.Web;
using System.Web.Optimization;

namespace SAMS
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
          "~/Scripts/jquery-ui-{version}.js"));  

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/adminlte.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome/css/font-awesome.css",
                        "~/Content/_all-skins.css",
                          "~/Content/AdminLTE.css",
                          "~/Content/font-awesome.css",
                          "~/Content/ionicons.css",
                          "~/Content/ionicons.css",
                          "~/Content/AdminLTE.css",
                            "~/Content/bootstrap.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
       "~/Content/themes/base/jquery-ui.css")); 
        }
    }
}
