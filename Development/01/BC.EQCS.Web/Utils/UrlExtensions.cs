using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace BC.EQCS.Web.Utils
{
    public static class UrlExtensions
    {
        public static string GetHrefFromRouteName(this UrlHelper url, string routeName, object routeValues = null)
        {
            var link = url.Link(routeName, routeValues);

            if (!WebAppSettings.UseHttps)
            {
                return link;
            }

            var regex = new Regex(@"(http|https):\/\/(.*)");

            var value = regex.Replace(link, @"https://$2");

            return value;
        }
    }
}