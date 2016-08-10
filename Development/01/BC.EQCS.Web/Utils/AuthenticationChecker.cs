using System.Configuration;

namespace BC.EQCS.Web.Utils
{
    /// <summary>
    /// This is a temporary class to allow legacy Authentcation OWIN Middleware and new Cookie Authentication to coexist in the solution and make them swappable. 
    /// </summary>
    public static class AuthenticationChecker
    {

        public static bool UseWindowsAuthOnly()
        {
            bool useWindowsAuthOnly;

            var configSetting = ConfigurationManager.AppSettings["UseWindowsAuthOnly"];
            bool.TryParse(configSetting, out useWindowsAuthOnly);
            return useWindowsAuthOnly;
        }
    }
}