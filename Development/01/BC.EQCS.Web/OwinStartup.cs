using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Web;
using BC.EQCS.Web.DependencyResolution;
using BC.EQCS.Web.Infrastructure.Logging;
using BC.EQCS.Web.Utils;
using BC.Security.Internal;
using BC.StructureMap.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using DefaultRegistry = BC.EQCS.Web.DependencyResolution.DefaultRegistry;
using AppFunc = Antlr.Runtime.Misc.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;


[assembly: OwinStartup(typeof(OwinStartup))]
namespace BC.EQCS.Web
{
    public class OwinStartup
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        public virtual void Configuration(IAppBuilder app)
        {
 		    var config = new HttpConfiguration();

            ConfigureWebApiDependencyInjection(app, config);
            ConfigureAuthentication(app, config);
            ConfigureOwin(app, config);
            ConfigureWebApiRoutes(app, config);
        }

        public virtual void ConfigureAuthentication(IAppBuilder app, HttpConfiguration config)
        {
            var cookieProvider = new CookieAuthenticationProvider
            {
                OnValidateIdentity = SecurityStampValidator
                    .OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentityCallback: (manager, user) =>
                            user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (id.GetUserId<int>()))
            };

            // Modify redirect behaviour to convert login URL to relative
            var applyRedirect = cookieProvider.OnApplyRedirect;
            cookieProvider.OnApplyRedirect = context =>
            {
                if (context.RedirectUri.StartsWith("http://" + context.Request.Host))
                {
                    context.RedirectUri = context.RedirectUri.Substring(
                        context.RedirectUri.IndexOf('/', "http://".Length));
                }
                applyRedirect(context);
            };


                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "ApplicationCookie",
                    LoginPath = new PathString(AuthenticationChecker.UseWindowsAuthOnly() ? "/Account/LoginAuto" : "/Account/Login"),
                    CookieHttpOnly = true,
                    SlidingExpiration = true,
                    //CookieSecure = CookieSecureOption.Always // use this in Production
                    ExpireTimeSpan = TimeSpan.FromHours(24),

                    Provider = cookieProvider

                });
        }

        public virtual void ConfigureWebApiDependencyInjection(IAppBuilder app, HttpConfiguration config)
        {
            StructureMapOptions structureMapOptionsForWebApi =
                new StructureMapOptions
                {
                    ConfigurationExpression = c =>
                    {
                        c.AddRegistry<SecurityRegistryWebApi>();
                        c.AddRegistry<DefaultRegistry>();
                    }
                };

            

            app.UseStructureMap(structureMapOptionsForWebApi, config);

            app.CreatePerOwinContext(() => ApplicationUserManager.Create(new IdentityFactoryOptions<ApplicationUserManager>()));

        

        }

        public void ConfigureOwin(IAppBuilder app, HttpConfiguration config)
        {
            Mapper.SetUp();

            // lockdown all controllers by applying [Authorize] globally
            config.Filters.Add(new AuthorizeAttribute());

            // Enable Elmah Logging for WebApi
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLoggerWebApi());

            config.Filters.Add(new NoCacheFilter());
        }

        public virtual void ConfigureWebApiRoutes(IAppBuilder app, HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new IsoDateTimeConverter());


            // Web API routes
            config.Routes.IgnoreRoute("WindowsLogin", "Windows/WindowsLogin");

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });



            // todo: manage this for each environment
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingModel>("IncidentsListing");
            builder.EntitySet<IncidentsListingModel>("LiveIncidentsListing");
            builder.EntitySet<IncidentsListingModel>("ClosedIncidentsListing");
            builder.EntitySet<IncidentsListingModel>("ActiveActionIncidentsListing");
            builder.EntitySet<IncidentActivityListingModel>("IncidentActivityListing");
            builder.EntitySet<IncidentActionListingModel>("IncidentActionListing");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            app.UseWebApi(config);
        }

    }
}
