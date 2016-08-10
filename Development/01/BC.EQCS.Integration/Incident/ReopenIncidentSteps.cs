using System.Linq;
using System.Net;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class ReopenIncidentSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [When(@"incident is reopened")]
        public void ReopenIncident(Table table)
        {
            var client = new Client();

            var model = new IncidentReopeningModel();

            var parsedTable = table.Parse();

            parsedTable.MapToModel(model, rows => rows.First());

            var response = client.ReOpenIncident(_specContext.IncidentIdUnderTest, model);

            _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
            {
                LogType = IncidentActivityLogType.Reopening,
                Payload = model.Reason
            };

            _specContext.ClientReponse = response;
        }

        [Given(@"incident is reopened and response is (.*)")]
        [When(@"incident is reopened and response is (.*)")]
        public void ReopenIncidentAndTestResponse(HttpStatusCode statusCode, Table table)
        {
            ReopenIncident(table);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}
