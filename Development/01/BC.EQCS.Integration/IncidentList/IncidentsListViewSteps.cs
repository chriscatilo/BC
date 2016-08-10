using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.IncidentList
{
    [Binding]
    public class IncidentsListViewSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();
        private List<GivenIncident> listOfIncidents = new List<GivenIncident>();

        [Given(@"List of incidents create (.*)")]
        public void GivenListOfIncidentsCreateTypical(string label)
        {
            listOfIncidents = CreateIncidentsFromTables("Typical");
        }

        [Given(@"I have permission to (.*) them and response is (.*)")]
        public void DoAction(string action, HttpStatusCode statusCode)
        {
            foreach (var incident in listOfIncidents)
            {
                _specContext.GivenIncident = incident;
                SaveRaiseNewIncidents(action);

                incident.ForPersistence.Id = _specContext.IncidentIdUnderTest;
                _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            }
        }

        [Given(@"I can view all the above incidents except (REJECTED)")]
        [Then(@"I can view all the above incidents except (REJECTED)")]
        public void WhenICanViewAllTheAboveIncidentsWhichAreNotMarkedAs(string statusCode)
        {
            var client = new Client();

            var response = client.GetIncidentsList();

            if (response.IsSuccessStatusCode)
            {
                var o = JObject.Parse(response.Content.ReadAsAsync<object>().Result.ToString());
                var result = JsonConvert.DeserializeObject<List<IncidentsListingModel>>(o.SelectToken("value").ToString());

                result.ForEach(x => x.StatusCode.Should().NotBe(statusCode));
            }
            _specContext.ClientReponse = response;
        }

        public void SaveRaiseNewIncidents(string action)
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

        }

        public List<GivenIncident> CreateIncidentsFromTables(string label)
        {
            var listOfIncidents = new List<GivenIncident>();

            var table = _specContext.GivenTableOfIncidentsToPersist;

            foreach (var row in table.Rows)
            {
                if (row["Test Label"].EqualsCaseInsensitive(label))
                {
                    var forPersistence = new IncidentModel();
                    var forViewing = new IncidentViewModel();
                    row.MapToModel(forPersistence);
                    var model = new GivenIncident
                    {
                        ForPersistence = forPersistence,
                        ForViewing = forViewing
                    };

                    listOfIncidents.Add(model);
                }
            }

            return listOfIncidents;
        }

        [When(@"the above raised incients are marked as (rejected) and response is (.*)")]
        public void WhenTheAboveRaisedIncientsAreMarkedAs(string action, HttpStatusCode statusCode)
        {
            foreach (var incident in listOfIncidents)
            {
                _specContext.GivenIncident = incident;
                var model = new IncidentRejectionModel { Reason = "rejection test" };

                RejectIncidents(incident.ForPersistence.Id, model);

                _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
            }
        }

        public void RejectIncidents(int id, IncidentRejectionModel model)
        {
            var client = new Client();

            var response = client.RejectIncident(id, model);

            _specContext.ClientReponse = response;
        }

    }
}
