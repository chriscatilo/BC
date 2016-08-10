using System;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.IncidentAction
{
    [Binding]
    public class RaceConditionsSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        private IncidentActionModel UserAPersistedModel;
        private IncidentActionModel UserBPersistedModel;

        [Given(@"action labeled (.*)")]
        public void GivenActionLabeled(string label)
        {
            var model = _specContext.CreateGivenActionFromTables(label);
            _specContext.GivenIncidentAction = model;
        }

        [When(@"action is created and response is (.*)")]
        public void WhenActionIsCreatedAndResponseIs(HttpStatusCode statusCode)
        {
            CreateAction();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [When(@"action is created")]
        public void CreateAction()
        {
            var client = new Client();

            _specContext.GivenIncidentAction.ForPersistence.AssignedTo = new string[] { "1" };

            var response = client.CreateIncidentAction(
                _specContext.IncidentIdUnderTest,
                _specContext.GivenIncidentAction);

            if (response.IsSuccessStatusCode)
            {
                //var uri = response.Headers.Location;
                //Assert.That(uri, Is.Not.Null, "Unable to get action location from response");
                //_specContext.ActionUriUnderTest = uri;
                var result = response.Content.ReadAsAsync<IncidentActionModel>().Result;
                _specContext.ActionUriUnderTest = new Uri(string.Format("{0}/{1}", response.RequestMessage.RequestUri.AbsoluteUri, result.Id));

                //_specContext.ActionUriUnderTest = response.Content.
                _specContext.GivenIncidentAction.ForPersistence.Id = result.Id;
                _specContext.GivenIncidentAction.ForViewing.Id = result.Id;

                _specContext.ActionUnderTest = _specContext.GivenIncidentAction;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"action is retrieved and response is (.*)")]
        [Then(@"action is retrieved and response is (.*)")]
        public void WhenActionIsRetrievedAndResponseIs(HttpStatusCode statusCode)
        {
            RetrieveAction();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        public void RetrieveAction()
        {
            var client = new Client();

            var response = client.Get(_specContext.ActionUriUnderTest);

            Console.WriteLine("_specContext.ActionUriUnderTest = " + _specContext.ActionUriUnderTest.AbsoluteUri);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IncidentActionModel>().Result;

                _specContext.ActionRetrieved = result;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"action details are correct")]
        [Then(@"action details are correct")]
        public void WhenActionDetailsAreCorrect()
        {
            var expected = _specContext.ActionUnderTest.ForPersistence;
            var actual = _specContext.ActionRetrieved;

            var propertiesToExcludeAssersions = new[]
            {
                "Id", "IncidentId", "AssignedOn", "AssignedTo", "RowVersion", "Status"
            };

            expected.AssertThatObjectsAreSame(actual, propertiesToExcludeAssersions);
        }

        [When(@"UserA view the action")]
        [When(@"UserB view the action")]
        [When(@"UserA and UserB view the action")]
        public void WhenUserAViewTheAction()
        {
            UserAPersistedModel = _specContext.ActionRetrieved;
            UserBPersistedModel = _specContext.ActionRetrieved;
        }

        [When(@"action is updated by UserA with (.*) label and response is (.*)")]
        [Then(@"action is updated by UserB with (.*) label and response is (.*)")]
        public void WhenActionIsUpdatedByUserAWithLabelAndResponseIsOk(string label, HttpStatusCode statusCode)
        {
            UpdateAction(label, true);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        private void UpdateAction(string label, bool isConflict = false)
        {
            var client = new Client();

            var model = _specContext.CreateGivenActionFromTables(label);

            if (label.Equals("Action1"))
            {
                model.ForPersistence.RowVersion = UserAPersistedModel.RowVersion;
                model.ForPersistence.AssignedTo = new string[] { "2" };
                model.ForPersistence.IncidentId = UserAPersistedModel.IncidentId;
                model.ForPersistence.Id = UserAPersistedModel.Id;
            }
            else {
                model.ForPersistence.RowVersion = isConflict == true ? UserAPersistedModel.RowVersion : null;
                model.ForPersistence.AssignedTo = new string[] { "1", "2" };
                model.ForPersistence.IncidentId = UserBPersistedModel.IncidentId;
                model.ForPersistence.Id = UserBPersistedModel.Id;
            }

            _specContext.GivenIncidentAction = model;

            var response = client.Put(_specContext.ActionUriUnderTest,
                _specContext.GivenIncidentAction.ForPersistence);

            if (response.IsSuccessStatusCode)
            {
                _specContext.ActionUnderTest = _specContext.GivenIncidentAction;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"action is updated by UserB with (.*) label and response is (.*)")]
        public void WhenActionIsUpdatedByUserAWithLabelAndResponseIs(string label, HttpStatusCode statusCode)
        {
            UpdateAction(label);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        //[When(@"UserB view the action")]
        //public void WhenUserBViewTheAction()
        //{
        //    ScenarioContext.Current.Pending();
        //}

        //[Then(@"action is updated by UserB with Action(.*) label and response is (.*)")]
        //public void ThenActionIsUpdatedByUserBWithActionLabelAndResponseIs(int p0, HttpStatusCode status)
        //{
        //    ScenarioContext.Current.Pending();
        //}

        //[Then(@"action is retrieved and response is (.*)")]
        //public void ThenActionIsRetrievedAndResponseIs(HttpStatusCode status)
        //{
        //    ScenarioContext.Current.Pending();
        //}

        //[Then(@"action details are correct")]
        //public void ThenActionDetailsAreCorrect()
        //{
        //    ScenarioContext.Current.Pending();
        //}



    }
}
