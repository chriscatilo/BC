using System.Net;
using System.Web.Http;
using BC.EQCS.Web;
using BC.EQCS.Web.DependencyResolution;
using BC.Security.Internal;
using BC.StructureMap.Owin;
using Owin;
using DefaultRegistry = BC.EQCS.Web.DependencyResolution.DefaultRegistry;

namespace BC.EQCS.Integration.Startup
{
    public class Startup : OwinStartup
    {

        public override void Configuration(IAppBuilder app)
        {
            app.RunSelfHosted();
           base.Configuration(app);
        }

        public override void ConfigureAuthentication(IAppBuilder app, HttpConfiguration config)
        {
            app.UseExtendedWindowsAuthentication(config);

            //     StructuremapMvc.Start();
            var listener = (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

        }

        public override void ConfigureWebApiDependencyInjection(IAppBuilder app, HttpConfiguration config)
        {
            var structureMapOptionsForWebApi = new StructureMapOptions
            {
                ConfigurationExpression = c =>
                {
                    c.AddRegistry<SecurityRegistryWebApiIntegrationTesting>();
                    c.AddRegistry<DefaultRegistry>();
                }
            };

            app.UseStructureMap(structureMapOptionsForWebApi, config);
        }

        // public override void Configuration(IAppBuilder app)
       // {
       //     var httpConfiguration = new HttpConfiguration();
            
       //     app.RunSelfHosted();

       //   //  base.ConfigureOwinForIntegrationTesting(app, httpConfiguration);

            
       ////     StructuremapMvc.Start();
       //     var listener = (HttpListener)app.Properties["System.Net.HttpListener"];
       //     listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

       //     httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
       // }
    }

}