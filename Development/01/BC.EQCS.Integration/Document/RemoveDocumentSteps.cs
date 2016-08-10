using System;
using System.Net;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Document
{
    [Binding]
    public class RemoveDocumentSteps
    {
        private readonly DocumentSpecFlowContextWrapper _specContext = new DocumentSpecFlowContextWrapper();

        [When(@"I remove file")]
        public void WhenIRemoveFile()
        {
            var id = (int)_specContext.Location.ExtractIdFromLocation();
             var client = new Client();
             var response = client.RemoveDocument(id);
             
            if (response.IsSuccessStatusCode)
             {
                 _specContext.GetDocumentModel(response);
             }
             else
             {
                 _specContext.InValidResponse(response.Content.ReadAsAsync<object>().Result);
             }
        }

        [When(@"I try to get the removed document")]
        public void WhenITryToGetTheRemovedDocument()
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

        [Then(@"I should get response (.*)")]
        public void ThenIShouldGetFileNotFoundResponse(HttpStatusCode statusCode)
        {
            _specContext.ClientReponse.AssertStatusCodeEquals(statusCode);
        }

    }
}
