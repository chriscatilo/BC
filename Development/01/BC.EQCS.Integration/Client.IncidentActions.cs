using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Integration
{
    public partial class Client
    {
        public HttpResponseMessage GetIncidentsList()
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append("/odata/IncidentsListing");

                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage GetIncidentForViewing(int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentById.Route, id);

                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage GetIncidentForPersistance(int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdPersistence.Route, id);

                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage GetIncidentActivityLog(int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdActivityLog.Route, id);

                var response = httpClient.GetAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage SaveNewIncident(GivenIncident model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append(ApiRoutes.Incident.Route);

                var response = httpClient.PostAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }

        public HttpResponseMessage SaveModifiedIncident(GivenIncident model, int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentById.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }

        public HttpResponseMessage RaiseNewIncident(GivenIncident model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.Append(ApiRoutes.IncidentSubmission.Route);

                var response = httpClient.PostAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }

        public HttpResponseMessage RaiseModifiedIncident(GivenIncident model, int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdSubmission.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, model.ForPersistence).Result;

                return response;
            }
        }

        public HttpResponseMessage DeleteIncident(int id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentById.Route, id);

                var response = httpClient.DeleteAsync(uri).Result;

                return response;
            }
        }

        public HttpResponseMessage AcceptIncident(int id, IncidentModel model = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdAcceptance.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, model).Result;

                return response;
            }
        }

        public HttpResponseMessage RejectIncident(int id, IncidentRejectionModel model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdRejection.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, model).Result;

                return response;
            }
        }

        public HttpResponseMessage CloseIncident(int id, IncidentAndWorkflowPayload<IncidentClosureModel> payload)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdClosure.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, payload).Result;

                return response;
            }
        }

        public HttpResponseMessage ReOpenIncident(int id, IncidentReopeningModel model)
        {
            using (var httpClient = CreateHttpClient())
            {
                var uri = HostUri.AppendWithId(ApiRoutes.IncidentByIdReopening.Route, id);

                var response = httpClient.PutAsJsonAsync(uri, model).Result;

                return response;
            }
        }
    }
}