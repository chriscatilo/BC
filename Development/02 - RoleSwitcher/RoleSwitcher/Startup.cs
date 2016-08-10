using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoleSwitcher.Startup))]
namespace RoleSwitcher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
