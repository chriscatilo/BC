using System;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Integration
{
    public partial class Client
    {
        public HttpResponseMessage UploadDocument(DocumentModel model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append(ApiRoutes.Document.Route);

                var response = httpClient.PostAsJsonAsync(uri, model).Result;

                return response;
            }
        }

        public HttpResponseMessage GetDocument(Uri uri)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = httpClient.GetAsync(uri.ToString().Replace("?id=", "/")).Result;
                return response;
            }
        }

        public Uri GetUri()
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append(ApiRoutes.Document.Route);
                return uri;
            }
        }

        public HttpResponseMessage RemoveDocument(int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.DeleteDocumentById.Route, id);

                var response = httpClient.DeleteAsync(uri).Result;

                return response;
            }
        }
    }
}
