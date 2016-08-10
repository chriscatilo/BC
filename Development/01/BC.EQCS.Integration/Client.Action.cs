using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;

namespace BC.EQCS.Integration
{
    public partial class Client
    {
        public HttpResponseMessage CreateIncidentAction(int incidentId, GivenIncidentAction model)
        {
            using (var httpClient = CreateHttpClient())
            {
                // '/api/IncidentAction', model
                //var uri = HostUri.Append(ApiRoutes.IncidentByIdAction.Route.Replace("{id:int}", incidentId.ToString()));

                model.ForPersistence.IncidentId = incidentId;
                model.ForViewing.IncidentId = incidentId;

                var uri = HostUri.Append("/api/IncidentAction");

                var response = httpClient.PostAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }
    }
}
