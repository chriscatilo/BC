using System;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BC.EQCS.Web.App_Start;
using HibernatingRhinos.Profiler.Appender.EntityFramework;

namespace BC.EQCS.Web
{
    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            this.EndRequest += (object sender, EventArgs e) =>
            {
                var isWindowsAuthRequested = HttpContext.Current.Response.Headers.Get("WindowsAuthRequested");

                if (!string.IsNullOrWhiteSpace(isWindowsAuthRequested) && isWindowsAuthRequested == "true")
                {
                    HttpContext.Current.Response.StatusCode = 401;
                    HttpContext.Current.Response.SubStatusCode = 2;
                }


            };
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

#if DEBUG
            EntityFrameworkProfiler.Initialize();
#endif

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }

        private void Application_Error(object sender, EventArgs e)
        {
            if ((((HttpApplication)(sender)).Context.Error).Message.ToUpper() == "UNKNOWN USER")
            {
                Server.ClearError();

                Response.Redirect("~/UserUnknown.html");
            }
        }

        //protected void Application_EndRequest(Object sender, EventArgs e)
        //{ 
        //    HttpContext context = HttpContext.Current;
        //    if (context.Response.Status.Substring(0,3).Equals("401"))
        //    {
        //    context.Response.ClearContent();
        //    context.Response.Write("<a href='http://localhost:52429/account/login'>go to login</a>");
        //    } 
        //}
    }
}