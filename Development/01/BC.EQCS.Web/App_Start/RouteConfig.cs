using System.Web.Mvc;
using System.Web.Routing;

namespace BC.EQCS.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Windows/WindowsLogin");


            routes.MapRoute(
                name: "ControllerOnly",
                url: "{controller}",
                defaults: new { action = "Index" });

            routes.MapRoute(
                name: "ControllerArgs",
                url: "{controller}/{*args}",
                defaults: new { action = "Index" }
            );

            routes.MapRoute(
                name: "Otherwise",
                url: "{*dynamicRoute}",
                defaults: new { controller = "Main", action = "Index", }
            );

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );

        }
    }
}
