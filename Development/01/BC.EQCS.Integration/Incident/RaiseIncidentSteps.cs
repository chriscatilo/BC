using System.Linq;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.UnitTests.Utils;
using BC.EQCS.Utils;
using BC.EQCS.Web.Models.Api;
using FastMember;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public sealed class RaiseIncidentSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [Given(@"incident labeled (.*)")]
        public void StoreGivenIncidentToPersist(string label)
        {
            var model = _specContext.CreateGivenIncidentFromTables(label);

            _specContext.GivenIncident = model;
        }

        [Given(@"new incident is (saved|raised)")]
        [When(@"new incident is (saved|raised)")]
        public void SaveRaiseNewIncident(string action)
        {
            var client = new Client();

            var response = action.EqualsCaseInsensitive("saved")
                ? client.SaveNewIncident(_specContext.GivenIncident)
                : client.RaiseNewIncident(_specContext.GivenIncident);

            if (response.IsSuccessStatusCode)
            {
                var uri = response.Headers.Location;

                var incidentId = uri.ExtractIdFromLocation();

                Assert.That(incidentId, Is.Not.Null, "Unable to extract the incident id from response's location value");

                _specContext.IncidentUnderTest = _specContext.GivenIncident;

                _specContext.IncidentIdUnderTest = incidentId ?? 0;
            }

            _specContext.ClientReponse = response;

            if (action.EqualsCaseInsensitive("raised"))
            {
                _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
                {
                    LogType = IncidentActivityLogType.Submission,
                    Payload = null
                };
            }
        }

        [Given(@"new incident is (saved|raised) and response is (.*)")]
        [When(@"new incident is (saved|raised) and response is (.*)")]
        public void SaveRaiseNewIncidentAndTestResponse(string action, HttpStatusCode statusCode)
        {
            SaveRaiseNewIncident(action);
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"incident is retrieved")]
        [When(@"incident is retrieved")]
        public void RetrieveIncident()
        {
            var client = new Client();

            var response = client.GetIncidentForViewing(_specContext.IncidentIdUnderTest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<GetIncidentResult>().Result;

                _specContext.IncidentRetrieved = result;
            }

            _specContext.ClientReponse = response;
        }

        [Given(@"incident is retrieved and response is (.*)")]
        [When(@"incident is retrieved and response is (.*)")]
        public void RetrieveIncidentAndTestResponse(HttpStatusCode statusCode)
        {
            RetrieveIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        //[Given(@"incident schema is retrieved")]
        //[When(@"incident schema is retrieved")]
        //public void RetrieveIncidentSchema()
        //{
        //    var client = new Client();

        //    var response = client.Get(_specContext.IncidentRetrieved.Schema);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var incident = response.Content.ReadAsAsync<GetIncidentSchemaResult>().Result;

        //        _specContext.IncidentSchemaRetrieved = incident;
        //    }

        //    _specContext.ClientReponse = response;
        //}

        //[Given(@"incident schema is retrieved and response is (.*)")]
        //[When(@"incident schema is retrieved and response is (.*)")]
        //public void RetrieveIncidentSchemaAndTestResponse(HttpStatusCode statusCode)
        //{
        //    RetrieveIncidentSchema();
        //    _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        //}

        [Given(@"incident is modified with (.*)")]
        [When(@"incident is modified with (.*)")]
        public void ModifyCurrentIncident(string label)
        {
            var model = _specContext.CreateGivenIncidentFromTables(label);

            _specContext.IncidentUnderTest = model;
        }

        [Given(@"incident is (saved|raised)")]
        [When(@"incident is (saved|raised)")]
        public void SaveRaiseCurrentIncident(string action)
        {
            var client = new Client();

            var model = _specContext.IncidentUnderTest;

            var incidentId = _specContext.IncidentIdUnderTest;

            var response = action.EqualsCaseInsensitive("raised")
                ? client.RaiseModifiedIncident(model, incidentId)
                : client.SaveModifiedIncident(model, incidentId);

            _specContext.ClientReponse = response;

            if (action.EqualsCaseInsensitive("raised"))
            {
                _specContext.IncidentWorkflowActivityUnderTest = new IncidentActivityLogModel
                {
                    LogType = IncidentActivityLogType.Submission,
                    Payload = null
                };
            }
        }

        [Given(@"incident is (saved|raised) and response is (.*)")]
        [When(@"incident is (saved|raised) and response is (.*)")]
        public void SaveRaiseCurrentIncidentAndTestResponse(string action, HttpStatusCode statusCode)
        {
            SaveRaiseCurrentIncident(action);
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Then(@"incident details are correct")]
        public void TestIncidentDetailsAreCorrect()
        {
            var expected = _specContext.IncidentUnderTest.ForViewing;
            var actual = _specContext.IncidentRetrieved.Model;

            var propertiesToExcludeAssersions = new[]
            {
                "Id", "FormalId", "Status", "AvailableCommands", "ActivityLog", "TestCentreAddress",
                "CreateDate", // TODO Chris: find a way of testing this 
                "RaisedDate", // TODO Chris: find a way of testing this 
                "LoggedByUser", // TODO Chris: find a way of testing this
                "LoggedByUserRole" // TODO Chris: find a way of testing this
            };

            expected.AssertThatObjectsAreSame(actual, propertiesToExcludeAssersions);
        }

        [Then(@"incident status is (.*)")]
        public void TestIncidentStatus(IncidentStatus expectedStatus)
        {
            Assert.That(_specContext.IncidentRetrieved.Model.Status, Is.EqualTo(expectedStatus));
        }

        [Then(@"available commands for incident are (.*)")]
        public void TestAvailableCommands(string commaSeparatedValues)
        {
            var commands = commaSeparatedValues.Split(',').Select(s => s.Trim());

            var incident = _specContext.IncidentRetrieved;

            var actual = incident.Commands.Select(item => item.Name.ToString());

            Assert.That(actual, Is.EquivalentTo(commands).IgnoreCase);

            incident.Commands.ToList().ForEach(cmd =>
            {
                Assert.That(cmd.Href, Is.Not.Null, string.Format("Command {0} is empty", cmd.Name));
            });
        }

        [Then(@"available commands for incident is empty")]
        public void TestEmptyAvailableCommands()
        {
            var commands = _specContext.IncidentRetrieved;

            var actual = commands.Commands.Select(item => item.Name.ToString());

            Assert.That(actual, Is.Empty);
        }


        [When(@"incident attribute (.*) is (.*)")]
        [Then(@"incident attribute (.*) is (.*)")]
        public void TestIncidentPropertyValueIsEqualTo(string propertyName, string value)
        {
            value = value.EqualsCaseInsensitive("NULL") ? null : value;

            var modelAccessor = ObjectAccessor.Create(_specContext.IncidentRetrieved.Model);

            var actual = modelAccessor[propertyName];

            Assert.That(actual, Is.EqualTo(value).IgnoreCase);
        }

    }
}