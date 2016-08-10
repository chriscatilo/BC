using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.UnitTests.Utils;
using BC.EQCS.Web.Models.Api;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Incident
{
    [Binding]
    public class ManageCandidatesSteps
    {
        private readonly IncidentSpecFlowContextWrapper _specContext = new IncidentSpecFlowContextWrapper();

        [Given(@"candidate labeled (.*)")]
        public void GivenCandidateLabeled(string label)
        {
            var model = _specContext.CreateGivenCandidateFromTables(label);
            _specContext.GivenIncidentCandidate = model;
        }

        [When(@"candidate is created and response is (.*)")]
        [Given(@"candidate is created and response is (.*)")]
        public void CreateCandidateAndCheckResponse(HttpStatusCode statusCode)
        {
            CreateCandidate();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [When(@"candidate is created")]
        public void CreateCandidate()
        {
            var client = new Client();

            var response = client.CreateIncidentCandidate(
                _specContext.IncidentIdUnderTest,
                _specContext.GivenIncidentCandidate);

            if (response.IsSuccessStatusCode)
            {
                var uri = response.Headers.Location;

                Assert.That(uri, Is.Not.Null, "Unable to get candidate location from response");

                _specContext.CandidateUriUnderTest = uri;

                _specContext.CandidateUnderTest = _specContext.GivenIncidentCandidate;
            }

            _specContext.ClientReponse = response;
        }

        [Given(@"candidate is retrieved and response is (.*)")]
        [When(@"UserA and UserB view the candidate")]
        [When(@"candidate is retrieved and response is (.*)")]
        [Then(@"candidate is retrieved and response is (.*)")]
        public void RetrieveCandidateAndTestResponse(HttpStatusCode statusCode)
        {
            RetrieveCandidate();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"candidate is retrieved")]
        [When(@"candidate is retrieved")]
        public void RetrieveCandidate()
        {
            var client = new Client();

            var response = client.Get(_specContext.CandidateUriUnderTest);

            Console.WriteLine("_specContext.CandidateUriUnderTest = " + _specContext.CandidateUriUnderTest.AbsoluteUri);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<CandidateResult>().Result;

                _specContext.CandidateRetrieved = result;
            }

            _specContext.ClientReponse = response;
        }

        [Given(@"all candidate for incident are retrieved and response is (.*)")]
        [When(@"all candidate for incident are retrieved and response is (.*)")]
        public void RetrieveAllCandidateAndTestResponse(HttpStatusCode statusCode)
        {
            RetrieveAllCandidate();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [Given(@"all candidates for incident are retrieved")]
        [When(@"all candidate for incident are retrieved")]
        public void RetrieveAllCandidate()
        {
            var client = new Client();

            var response = client.GetAllCandidatesForIncident(_specContext.IncidentIdUnderTest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IEnumerable<CandidateResult>>().Result;

                _specContext.CandidatesRetrieved = result;
            }

            _specContext.ClientReponse = response;
        }

        [Then(@"candidate details are correct")]
        [When(@"candidate details are correct")]
        public void TestCandidateDetailsAreCorrect()
        {
            var expected = _specContext.CandidateUnderTest.ForViewing;
            var actual = _specContext.CandidateRetrieved.Model;

            var propertiesToExcludeAssersions = new[]
            {
                "Id", "IncidentId"
            };

            expected.AssertThatObjectsAreSame(actual, propertiesToExcludeAssersions);
        }

        [When(@"candidate is deleted and response is (.*)")]
        [Given(@"candidate is deleted and response is (.*)")]
        public void DeleteCandidateAndCheckResponse(HttpStatusCode statusCode)
        {
            DeleteCandidate();
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [When(@"candidate is deleted")]
        public void DeleteCandidate()
        {
            var client = new Client();

            var response = client.Delete(_specContext.CandidateUriUnderTest);

            _specContext.ClientReponse = response;
        }

        [Then(@"candidates retrieved are")]
        public void ThenCandidatesRetrievedAre(Table table)
        {
            var expectedNumbers = table.Rows.SelectMany(row => row.Values);

            var actualNumbers = _specContext.CandidatesRetrieved.Select(item => item.Model.Number);

            Assert.That(expectedNumbers, Is.EquivalentTo(actualNumbers));
        }

        [When(@"candidate is updated with (.*) label and response is (.*)")]
        [Given(@"candidate is updated with (.*) label and response is (.*)")]
        public void UpdateCandidateAndCheckResponse(string label, HttpStatusCode statusCode)
        {
            UpdateCandidate(label);

            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

        [When(@"candidate is updated with (.*) label")]
        [Given(@"candidate is updated with (.*) label")]
        public void UpdateCandidate(string label)
        {
            var client = new Client();

            // update _specContext.GivenIncidentCandidate
            GivenCandidateLabeled(label);

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