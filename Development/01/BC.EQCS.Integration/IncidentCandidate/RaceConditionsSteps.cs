using System;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Incident;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.IncidentCandidate
{
    [Binding]
    public class IncidentRaceConditionsSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        private IncidentCandidateModel _userACandidateResult;
        private IncidentCandidateModel _userBCandidateResult;

        [When(@"UserA and UserB view the candidate")]
        [When(@"UserA view the candidate")]
        [When(@"UserB view the candidate")]
        public void WhenUserAViewTheCandidate()
        {
            GetCandidatePersisted(_specContext.CandidateRetrieved.Persisted);
        }

        private void GetCandidatePersisted(string uri)
        {
            var client = new Client();

            var response = client.Get(uri);

            Console.WriteLine("_specContext.CandidateUriUnderTest = " + _specContext.CandidateUriUnderTest.AbsoluteUri);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IncidentCandidateModel>().Result;

                _userACandidateResult = result;
                _userBCandidateResult = result;
            }

            _specContext.ClientReponse = response;
        }

        [When(@"candidate is updated by UserA with (.*) label and response is (.*)")]
        [When(@"candidate is updated by UserB with (.*) label and response is (.*)")]
        [Then(@"candidate is updated by UserB with (.*) label and response is (.*)")]
        public void WhenCandidateIsUpdatedByUserAWithLabelAndResponseIsOk(string label, HttpStatusCode statusCode)
        {
            UpdateCandidate(label);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        private void UpdateCandidate(string label)
        {
            var client = new Client();

            var model = _specContext.CreateGivenCandidateFromTables(label);
            model.ForPersistence.RowVersion = _userACandidateResult.RowVersion;
            _specContext.GivenIncidentCandidate = model;

            var response = client.Put(_specContext.CandidateUriUnderTest,
                _specContext.GivenIncidentCandidate.ForPersistence);

            if (response.IsSuccessStatusCode)
            {
                _specContext.CandidateUnderTest = _specContext.GivenIncidentCandidate;
            }

            _specContext.ClientReponse = response;
        }
    }
}