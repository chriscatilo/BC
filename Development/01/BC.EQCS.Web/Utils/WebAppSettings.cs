using System.Configuration;

namespace BC.EQCS.Web.Utils
{
    public static class WebAppSettings
    {
        public static readonly bool UseHttps;

        public static string GetCurrentHttpProtocol
        {
            get { return UseHttps ? "https" : "http"; }
        }

        static WebAppSettings()
        {
            UseHttps = bool.Parse(ConfigurationManager.AppSettings["UseHttps"] ?? "false");
        }
    }
}