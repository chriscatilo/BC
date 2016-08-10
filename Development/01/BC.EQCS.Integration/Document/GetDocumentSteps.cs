using System;
using System.Net.Http;
using BC.EQCS.Web.Models;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Document
{
    public class GetDocumentSteps
    {
        private readonly DocumentSpecFlowContextWrapper _specContext = new DocumentSpecFlowContextWrapper();

        [Given(@"A Valid Document")]
        public void GivenAValidDocument()
        {
            _specContext.GivenValidDocument = _specContext.CreateValidDocument();
        }

        [When(@"I press upload '(.*)'")]
        public void WhenIPressUpload(string type)
        {
            var client = new Client();
            var response = client.UploadDocument(_specContext.GivenValidDocument);

            _specContext.ClientReponse = response;

            if (response.IsSuccessStatusCode)
            {
                _specContext.ValidResponse(response);
            }
            else
            {
                _specContext.InValidResponse(response.Content.ReadAsAsync<object>().Result);
            }
        }

        [Then(@"I should get a valid url")]
        public void ThenIShouldGetAValidUrl()
        {
            _specContext.Location.ToString().Should().Contain(ApiRoutes.Document.Route);
            _specContext.Success.Should().BeTrue();
        }

        [When(@"I try to get the above saved document")]
        public void WhenITryToGetTheAboveSavedDocument()
        {
            var client = new Client();
            var response = client.GetDocument(_specContext.Location);
            if (response.IsSuccessStatusCode)
            {
                _specContext.GetDocumentModel(response);
            }
            else
            {
                _specContext.InValidResponse(response.Content.ReadAsAsync<object>().Result);
            }
        }

        [Then(@"I should get back the above saved document")]
        public void ThenIShouldGetBackTheAboveSavedDocument()
        {
            _specContext.ResultantDocumentModel.ShouldBeEquivalentTo(_specContext.GivenValidDocument);
        }

        [Given(@"A InValid URI")]
        public void GivenAInValidURI()
        {
            var client = new Client();
            var uri = client.GetUri();            
            _specContext.Location = new Uri(uri + "/9999");
        }

        [When(@"I try to get Non Existing Document")]
        public void WhenITryToGetNonExistingDocument()
        {
            var client = new Client();
            var response = client.GetDocument(_specContext.Location);
            if (response.IsSuccessStatusCode)
            {
                _specContext.GetDocumentModel(response);
            }
            else
            {
                _specContext.InValidResponse(response.Content.ReadAsAsync<object>().Result);
            }
        }

        [Then(@"I should get nothing")]
        public void ThenIShouldGetNothing()
        {
            _specContext.ResultantDocumentModel.Should().BeNull();
            _specContext.Success = true;
        }

    }
}
