using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace BC.Security.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class WindowsPrincipalAuthenticationMiddleware :
        AuthenticationMiddleware<WindowsPrincipalAuthenticationOptions>
    {
        private readonly IWindowsPrincipalHandlerFactory _principalHandlerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <param name="principalHandlerFactory"></param>
        public WindowsPrincipalAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, WindowsPrincipalAuthenticationOptions options, IWindowsPrincipalHandlerFactory principalHandlerFactory)
            : base(next, options)
        {
            _principalHandlerFactory = principalHandlerFactory;
            if (options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(typeof(WindowsPrincipalAuthenticationMiddleware).FullName,
                    WindowsPrincipalAuthenticationOptions.OptionsAuthenticationType);

                options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }
        }

        protected override AuthenticationHandler<WindowsPrincipalAuthenticationOptions> CreateHandler()
        {
            return
                _principalHandlerFactory.Create();
        }
    }
}