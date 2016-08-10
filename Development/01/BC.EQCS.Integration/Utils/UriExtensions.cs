using System;
using System.Linq;

namespace BC.EQCS.Integration.Utils
{
    public static class UriExtensions
    {
        public static int? ExtractIdFromLocation(this Uri uri)
        {
            string resource = uri.Segments.Last();

            int id;

            return int.TryParse(resource, out id)
                       ? id
                       : default(int?);
        }

        public static Uri Append(this Uri left, string right)
        {
            var uri = new Uri(left, right);

            return uri;
        }

        public static Uri AppendWithId(this Uri left, string right, int id)
        {
            var relative = right.Replace("{id:int}", id.ToString());

            var uri = new Uri(left, relative);

            return uri;
        }
    }
}