using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Utils
{
    public class ValidationFailedResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly BadRequestMessage _value;

        public ValidationFailedResult(BadRequestMessage badRequestMessage, HttpRequestMessage request)
        {
            _value = badRequestMessage;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = _request.CreateResponse(HttpStatusCode.BadRequest, _value);
            return Task.FromResult(response);
        }
    }
}