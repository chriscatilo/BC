using System;
using System.DirectoryServices;

namespace BC.EQCS.Repositories.Utils
{
    public static class SearchResultExtensions
    {
        public static string GetSearchResultPropertyAsString(this SearchResult searchResult, string propertyName)
        {
            if (searchResult.Properties != null &&
                searchResult.Properties.Count > 0 &&
                searchResult.Properties[propertyName] != null &&
                searchResult.Properties[propertyName].Count > 0
                )
            {
                return searchResult.Properties[propertyName][0].ToString();
            }

            return string.Empty;
        }

        public static Guid GetSearchResultPropertyAsGuid(this SearchResult searchResult, string propertyName)
        {
            if (searchResult.Properties != null &&
                searchResult.Properties.Count > 0 &&
                searchResult.Properties[propertyName] != null &&
                searchResult.Properties[propertyName].Count > 0
                )
            {
                var bytes = (byte[])searchResult.Properties[propertyName][0];
                return new Guid(bytes);
            }

            return Guid.Empty;
        }
    }
}
