using System.Linq;
using System.Net;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Web.Models;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class CloseIncidentSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [Given(@"incident is closed")]
        [When(@"incident is closed")]
        public void CloseIncident(Table table)
        {
            var client = new Client();

            var closureModel = new IncidentClosureModel();

            var parsedTable = table.Parse();

            // map comment value to incident
            parsedTable.MapToModel(closureModel, rows => rows.First());

            var incidentModel = _specContext.IncidentUnderTest.ForPersistence;
            
            // map residual risk value to incident
            parsedTable.MapToModel(incidentModel, rows => rows.First());

            var payload = PayloadFactory.Create(incidentModel, closureModel);

            var response = client.CloseIncident(_specContext.IncidentIdUnderTest, payload);

            _specContext.ClientReponse = response;

            _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
            {
                LogType = IncidentActivityLogType.Closure,
                Payload = payload.WorkflowModel.Comments
            };

            // pick up residual risk from the table and put it in the incident model under test
            parsedTable.MapToModel(_specContext.IncidentUnderTest, rows => rows.First());
        }

        [Given(@"incident is closed and response is (.*)")]
        [When(@"incident is closed and response is (.*)")]
        public void CloseIncidentAndTestResponse(HttpStatusCode statusCode, Table table)
        {
            CloseIncident(table);
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"incident is updated with row (.*) while closing")]
        [When(@"incident is updated with row (.*) while closing")]
        public void UpdateAndCloseIncident(Table table, IncidentModel incidentModel)
        {
            var client = new Client();

            var closureModel = new IncidentClosureModel();

            var parsedTable = table.Parse();

            // map comments value to closure model
            parsedTable.MapToModel(closureModel, rows => rows.First());

            // map residual risk to incident model
            parsedTable.MapToModel(incidentModel, rows => rows.First());

            var payload = PayloadFactory.Create(incidentModel, closureModel);

            var response = client.CloseIncident(_specContext.IncidentIdUnderTest, payload);

            _specContext.ClientReponse = response;

            _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
            {
                LogType = IncidentActivityLogType.Closure,
                Payload = closureModel.Comments
            };

            // pick up residual risk from the table and put it in the incident model under test
            parsedTable.MapToModel(_specContext.IncidentUnderTest, rows => rows.First());
        }

        [When(@"incident is updated with row (.*) while closing and response is (.*)")]
        [Given(@"incident is updated with row (.*) while closing and response is (.*)")]
        public void UpdateAndCloseIncidentAndTestResponse(string label, HttpStatusCode statusCode, Table table)
        {
            var model = _specContext.CreateGivenIncidentFromTables(label);

            _specContext.IncidentUnderTest = model;

            UpdateAndCloseIncident(table, model.ForPersistence);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }
    }
}