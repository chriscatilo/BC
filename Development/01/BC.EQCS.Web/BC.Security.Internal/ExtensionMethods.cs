using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BC.Security.Internal.Contracts.Models;
using BC.StructureMap.Owin;
using Owin;

namespace BC.Security.Internal
{
    public static class ExtensionMethods
    {
        public static IAppBuilder UseExtendedWindowsAuthentication(this IAppBuilder app, HttpConfiguration configuration)
        {
            var options = new WindowsPrincipalAuthenticationOptions();

            UseStructureMap(app);

            // TODO: Consider replacing the container reference with a servicelocator wrapper.
            app.Use(typeof(WindowsPrincipalAuthenticationMiddleware), app, options,
                IoC.Container.GetInstance<IWindowsPrincipalHandlerFactory>());

            configuration.Filters.Add(new HostAuthenticationFilter(typeof(WindowsPrincipalAuthenticationMiddleware).ToString()));
            return app;
        }

        public static Task<UserModel> GetExtendedWindowsUser(this HttpRequestMessage request)
        {
            var owinContext = request.GetOwinContext();
            return Task.FromResult(owinContext.Get<UserModel>(WindowsPrincipalHandler.UserRequestKey));

        }

        private static void UseStructureMap(IAppBuilder app)
        {
            var structureMapOptions = new StructureMapOptions
            {
                ConfigurationExpression = c => c.AddRegistry<DefaultRegistry>()
            };

            app.UseStructureMap(structureMapOptions);
        }


    }
}
