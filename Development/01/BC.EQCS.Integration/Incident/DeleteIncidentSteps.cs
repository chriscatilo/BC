using System.Net;
using BC.EQCS.Integration.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class DeleteIncidentSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [When(@"incident is deleted")]
        public void DeleteIncident()
        {
            var client = new Client();

            var response = client.DeleteIncident(_specContext.IncidentIdUnderTest);

            _specContext.ClientReponse = response;
        }

        [When(@"incident is deleted and response is (.*)")]
        public void DeleteIncidentAndTestResponse(HttpStatusCode statusCode)
        {
            DeleteIncident();

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}