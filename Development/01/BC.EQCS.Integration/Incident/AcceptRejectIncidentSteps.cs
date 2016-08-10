using System.Linq;
using System.Net;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class AcceptRejectIncidentSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [Given(@"incident is accepted")]
        [When(@"incident is accepted")]
        public void AcceptIncident()
        {
            UpdateAndAcceptIncident();
        }

        [Given(@"incident is accepted and response is (.*)")]
        [When(@"incident is accepted and response is (.*)")]
        public void AcceptIncidentAndTestResponse(HttpStatusCode statusCode)
        {
            UpdateAndAcceptIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"incident is updated with row (.*) while accepting")]
        [When(@"incident is updated with row (.*) while accepting")]
        public void UpdateAndAcceptIncident(IncidentModel model = null)
        {
            var client = new Client();

            var response = client.AcceptIncident(_specContext.IncidentIdUnderTest, model);

            _specContext.ClientReponse = response;

            _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
            {
                LogType = IncidentActivityLogType.Acceptance,
                Payload = null
            };
        }

        [When(@"incident is updated with row (.*) while accepting and response is (.*)")]
        [Given(@"incident is updated with row (.*) while accepting and response is (.*)")]
        public void UpdateAndActionIncidentAndTestResponse(string label, HttpStatusCode statusCode)
        {
            var model = _specContext.CreateGivenIncidentFromTables(label);

            _specContext.IncidentUnderTest = model;

            UpdateAndAcceptIncident(model.ForPersistence);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"incident is rejected")]
        [When(@"incident is rejected")]
        public void RejectIncident(Table table)
        {
            var client = new Client();

            var model = new IncidentRejectionModel();

            var parsedTable = table.Parse();

            parsedTable.MapToModel(model, rows => rows.First());

            var response = client.RejectIncident(_specContext.IncidentIdUnderTest, model);

            _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
            {
                LogType = IncidentActivityLogType.Rejection,
                Payload = model.Reason
            };

            _specContext.ClientReponse = response;
        }

        [Given(@"incident is rejected and response is (.*)")]
        [When(@"incident is rejected and response is (.*)")]
        public void RejectIncidentAndTestReponse(HttpStatusCode statusCode, Table table)
        {
            RejectIncident(table);
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}