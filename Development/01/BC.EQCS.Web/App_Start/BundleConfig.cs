using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace BC.EQCS.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scripts = new List<ScriptBundle>
            {
                Js("~/bundles/jquery",
                   new[]
                   {
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-ui-{version}.js"
                   }),

                Js("~/bundles/angular",
                   new[]
                   {
                       "~/Scripts/angular.js",
                       "~/Scripts/angular-route.js",
                       "~/Scripts/angular-animate.js",
                       "~/Scripts/angular-resource.js",
                       "~/Scripts/angular-loxL-storage.js"
                   },
                   new[] {"~/Scripts/angular-ui/*.js"},
                   new[] {"~/Scripts/i18n/angular-locale_en-gb.js"}),

                Js("~/bundles/utils",
                   new[]
                   {
                       "~/Scripts/underscore.js"
                   }),

                Js("~/bundles/eqcs-app",
                   new[]
                   {
                       "~/app/core/eqcs.core.module.js",
                       "~/app/core/*.js",
                       "~/app/error/eqcs.error.module.js",
                       "~/app/error/*.js",
                       "~/app/main/eqcs.main.module.js",
                       "~/app/main/*.js",
                       "~/app/home/eqcs.home.module.js",
                       "~/app/home/*.js",
                       "~/app/login/*.js",
                       "~/app/incident/eqcs.incident.module.js",
                       "~/app/incident/eqcs.incident.init.js",
                       "~/app/incident/*.js",
                       "~/app/auditing/eqcs.auditing.module.js",
                       "~/app/auditing/*.js",
                       "~/app/useradmin/eqcs.useradmin.module.js",
                       "~/app/useradmin/*.js"
                   }),

                Js("~/bundles/thirdparty",
                   new[]
                   {
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/kendo/kendo.all.min.js",
                       "~/Scripts/kendo/cultures/kendo.culture.en-GB.min.js",
                       "~/Scripts/toaster.js",
                       "~/Scripts/jquery.visible.js",
                       "~/Scripts/wcAngularOverlay.js"
                   }),
            };

            scripts.ForEach(bundle => bundles.Add(bundle));

            var styles = new List<StyleBundle>
            {
                Css("~/Content/css/eqcs",
                    new[]
                    {
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/bootswatch.min.css",
                        "~/Content/css/site.css",
                        "~/Content/css/kendo-overrides.css",
                        "~/Content/css/toaster.css"
                    }),

                Css("~/Content/css/kendo/eqcs",
                    new[]
                    {
                        "~/Content/css/kendo/kendo.common.min.css",
                        "~/Content/css/kendo/kendo.default.min.css"
                    })
            };

            styles.ForEach(bundle => bundles.Add(bundle));
        }

        private static ScriptBundle Js(string bundlePath, params string[][] includes)
        {
            var bundle = new ScriptBundle(bundlePath);

            includes.ToList().ForEach(include => bundle.Include(include));

            return bundle;
        }


        private static StyleBundle Css(string bundlePath, params string[][] includes)
        {
            var bundle = new StyleBundle(bundlePath);

            includes.ToList().ForEach(include => bundle.Include(include));

            return bundle;
        }
        
    }
}