using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Integration
{
    public partial class Client
    {
        public HttpResponseMessage CreateIncidentCandidate(int incidentId, GivenIncidentCandidate model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append(ApiRoutes.IncidentByIdCandidate.Route.Replace("{id:int}", incidentId.ToString()));

                var response = httpClient.PostAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }

        public HttpResponseMessage GetAllCandidatesForIncident(int incidentId)
        {
            using (var httpClient = CreateHttpClient())
            {
                var route = ApiRoutes.IncidentByIdCandidate.Route.Replace("{id:int}", incidentId.ToString());

                var uri = HostUri.Append(route);

                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }
    }
}