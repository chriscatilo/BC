using System;
using System.Net.Http;
using BC.EQCS.Integration.Utils;

namespace BC.EQCS.Integration
{
    // helper methods can be found in other partial class files nested in this file
    public partial class Client
    {
        protected Uri HostUri = new Uri(Constants.SelfHostUrl);

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient(new HttpClientHandler
            {
                UseDefaultCredentials = true
            });
        }

        public HttpResponseMessage Get(Uri uri)
        {
            return Get(uri.ToString());
        }

        public HttpResponseMessage Get(string uri)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage Delete(Uri uri)
        {
            return Delete(uri.ToString());
        }

        public HttpResponseMessage Delete(string uri)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = httpClient.DeleteAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage Put<TModel>(Uri uri, TModel model)
        {
            return Put(uri.ToString(), model);
        }

        public HttpResponseMessage Put<TModel>(string uri, TModel model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = httpClient.PutAsJsonAsync(uri, model).Result;

                return response;
            }
        }
    }
}
