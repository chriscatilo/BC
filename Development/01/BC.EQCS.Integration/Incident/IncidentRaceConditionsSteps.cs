using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using BC.EQCS.Web.Models.Api;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class IncidentRaceConditionsSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        private GetIncidentResult userAIncidentResult;
        private GetIncidentResult userBIncidentResult;

        private IncidentModel persistedUserAIncidentResult;
        private IncidentModel persistedUserBIncidentResult;

        private const string raceConditonMessage =
    "Unfortunately this Incident has been updated by another user, therefore your changes can no longer be applied. Please cancel these changes and re-open the record. If you wish to keep a copy of your data, you can utilities copy and paste functionality to save re-typing on the updated record.";


        [When(@"the incident is retrieved by UserA and UserB where response is (.*)")]
        public void WhenTheIncidentIsRetrievedByUserAAndUserBWhereResponseIsOk(HttpStatusCode statusCode)
        {
            RetrieveIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            userAIncidentResult = _specContext.IncidentRetrieved;
            userBIncidentResult = _specContext.IncidentRetrieved;

            RetrievePersistedIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            persistedUserAIncidentResult = _specContext.IncidentPersistedRetrieved;
            persistedUserBIncidentResult = _specContext.IncidentPersistedRetrieved;
        }

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

        public void RetrievePersistedIncident()
        {
            var client = new Client();

            var response = client.GetIncidentForPersistance(_specContext.IncidentIdUnderTest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IncidentModel>().Result;

                _specContext.IncidentPersistedRetrieved = result;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"the incident details are correct")]
        public void WhenTheIncidentDetailsAreCorrect()
        {
            var expected = _specContext.IncidentUnderTest.ForViewing;
            var actual = _specContext.IncidentRetrieved.Model;

            var propertiesToExcludeAssersions = new[]
             {
                 "Id", "FormalId", "Status", "AvailableCommands", "ActivityLog", "TestCentreAddress",
                 "CreateDate", "RaisedDate", "LoggedByUser", "LoggedByUserRole"
             };

            expected.AssertThatObjectsAreSame(actual, propertiesToExcludeAssersions);
        }

        [When(@"UserA modifies the retrieved incident (.*)")]
        [When(@"UserB modifies the retrieved incident (.*)")]
        public void WhenUserAModifiesTheRetrievedIncident(string label)
        {
            var model = _specContext.CreateGivenIncidentFromTables(label);
            if (label.Equals("UpdateByUserA"))
            {
                model.ForPersistence.Id = userAIncidentResult.Model.Id;
                model.ForPersistence.RowVersion = persistedUserAIncidentResult.RowVersion;
            }
            else if (label.Equals("UpdateByUserB"))
            {
                model.ForPersistence.Id = userBIncidentResult.Model.Id;
                model.ForPersistence.RowVersion = persistedUserBIncidentResult.RowVersion;
            }
            _specContext.IncidentUnderTest = model;
        }

        [When(@"the incident is retrieved by UserA where response is (.*)")]
        public void WhenTheIncidentIsRetrievedByUserAWhereResponseIs(HttpStatusCode statusCode)
        {
            RetrieveIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            userAIncidentResult = _specContext.IncidentRetrieved;

            RetrievePersistedIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            persistedUserAIncidentResult = _specContext.IncidentPersistedRetrieved;
        }

        [When(@"the incident is retrieved by UserB where response is (.*)")]
        public void WhenTheIncidentIsRetrievedByUserBWhereResponseIs(HttpStatusCode statusCode)
        {
            RetrieveIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            userBIncidentResult = _specContext.IncidentRetrieved;

            RetrievePersistedIncident();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

            persistedUserBIncidentResult = _specContext.IncidentPersistedRetrieved;
        }

        [Then(@"the response message is")]
        [When(@"the response message is")]
        public void ThenTheResponseMessageIs()
        {
            _specContext.ClientReponse.AssertErrorMessageEquals("RaceConditionConflict");
        }

        [When(@"UserB dones not modifiy the inciedent")]
        public void WhenUserBDonesNotModifiyTheInciedent()
        {
            //ScenarioContext.Current.Pending();
        }

        //[When(@"UserA modifies retrieved incident UpdateByUserA")]
        //public void WhenUserAModifiesRetrievedIncidentUpdateByUserA()
        //{
        //    RetrieveIncident();
        //    _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

        //    userAIncidentResult = _specContext.IncidentRetrieved;
        //    userBIncidentResult = _specContext.IncidentRetrieved;

        //    RetrievePersistedIncident();
        //    _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);

        //    persistedUserAIncidentResult = _specContext.IncidentPersistedRetrieved;
        //    persistedUserBIncidentResult = _specContext.IncidentPersistedRetrieved;
        //}

    }
}