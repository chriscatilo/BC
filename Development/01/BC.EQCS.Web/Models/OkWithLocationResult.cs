using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BC.EQCS.Web.Models
{
    public class OkWithLocationResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly string _location;

        public OkWithLocationResult(HttpRequestMessage request, string location)
        {
            _request = request;
            _location = location;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var okResult = new OkResult(_request);

            return
                okResult.ExecuteAsync(cancellationToken)
                        .ContinueWith(t =>
                            {
                                t.Result.Headers.Location = new Uri(_location);
                                return t.Result;
                            }, cancellationToken);
        }
    }
}