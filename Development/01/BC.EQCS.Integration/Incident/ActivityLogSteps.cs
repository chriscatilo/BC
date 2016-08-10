using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class ActivityLogSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [When(@"activity log of incident is retrieved")]
        [Given(@"activity log of incident is retrieved")]
        public void RetrieveIncidentActivityLog()
        {
            var client = new Client();

            var response = client.GetIncidentActivityLog(_specContext.IncidentIdUnderTest);

            if (response.IsSuccessStatusCode)
            {
                var log = response.Content.ReadAsAsync<IEnumerable<IncidentActivityLogModel>>().Result;

                _specContext.IncidentActivityLogRetrieved = log;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"activity log of incident is retrieved and response is (.*)")]
        [Given(@"activity log of incident is retrieved and response is (.*)")]
        public void RetrieveIncidentActivityLogAndTestResponse(HttpStatusCode statusCode)
        {
            RetrieveIncidentActivityLog();

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}