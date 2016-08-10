using System;
using System.Net.Http;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using BC.EQCS.Web.Models;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Document
{
    [Binding]
    public class CreateDocument_TestsSteps
    {
        private readonly DocumentSpecFlowContextWrapper _specContext = new DocumentSpecFlowContextWrapper();

        [Given(@"A Valid Document")]
        public void GivenAValidDocument()
        {
            _specContext.GivenValidDocument = _specContext.CreateValidDocument();
        }

        [Given(@"A Valid Document That Exceeds Ten MB Size")]
        public void GivenAValidDocumentThatExceedsTenMBSize()
        {
            _specContext.GivenValidDocumentExceeds10MB = _specContext.CreateValidDocumentExceeds10MB();
        }

        [Given(@"A InValid Document That Exceeds Ten MB Size")]
        public void GivenAInValidDocumentThatExceedsTenMBSize()
        {
            _specContext.GivenInValidDocument = _specContext.CreateInValidDocument();
        }

        [When(@"I press upload '(.*)'")]
        public void WhenIPressUpload(string type)
        {
            var client = new Client();
            HttpResponseMessage response = null;

            switch (type)
            {
                case "ValidDocument":
                case "DOC":
                case "DOCX":
                case "PDF":
                case "JPG":
                    {
                        response = client.UploadDocument(_specContext.GivenValidDocument);
                        break;
                    }
                case "ValidDocumentExceeds10MB":
                    {
                        response = client.UploadDocument(_specContext.GivenValidDocumentExceeds10MB);
                        break;
                    }

                case "InValidDocumentExceeds10MB":
                    {
                        response = client.UploadDocument(_specContext.GivenInValidDocument);
                        break;
                    }
                case "Empty-DOC":
                case "Empty-DOCX":
                case "Empty-PDF":
                case "Empty-JPG":
                    {
                        response = client.UploadDocument(_specContext.GivenValidEmptyDocument);
                        break;
                    }
            }

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

        [Then(@"I should get an exception '(.*)'")]
        public void ThenIShouldGetAnException(string exceptionFor)
        {
            _specContext.Failure.Should().BeTrue();

            switch (exceptionFor)
            {
                case "ValidDocumentExceeds10MB":
                    {
                        _specContext.ValidationFailureException.AssertValidationResultIncludes("File size should not exceed 10mb.");
                        break;
                    }

                case "InValidDocumentExceeds10MB":
                    {
                        _specContext.ValidationFailureException.AssertValidationResultIncludes("File size should not exceed 10mb.");
                        _specContext.ValidationFailureException.AssertValidationResultIncludes("Invalid format. Following are the supported file formats (pdf, doc, docx or jpg)");
                        break;
                    }
                case "Empty-DOC":
                case "Empty-DOCX":
                case "Empty-PDF":
                case "Empty-JPG":
                    {
                        _specContext.ValidationFailureException.AssertValidationResultIncludes("Empty file cannot be uploaded.");
                        break;
                    }
                case "FileName-Too-Long":
                    {
                        _specContext.ValidationFailureException.AssertValidationResultIncludes("File Name Too Long");
                        break;
                    }
            }
        }

        [Given(@"A Valid Document of Type '(.*)'")]
        public void GivenAValidDocumentOfType(string type)
        {
            _specContext.GivenValidDocument = new DocumentModel
            {
                ContentType = type.ToLower(),
                ContentName = string.Format("File.{0}", type.ToLower()),
                Content = new byte[9 * 1024]
            };
        }

        [Given(@"A Valid Empty Document of Type '(.*)'")]
        public void GivenAValidEmptyDocumentOfType(string type)
        {
            _specContext.GivenValidEmptyDocument = new DocumentModel
            {
                ContentType = type.ToLower(),
                ContentName = string.Format("File.{0}", type.ToLower()),
                Content = new byte[0]
            };
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
            _specContext.ResultantDocumentModel.ContentName.ShouldBeEquivalentTo(_specContext.GivenValidDocument.ContentName);
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

        [Given(@"A Valid Document of Type '(.*)' And FileName Too Long")]
        public void GivenAValidDocumentOfTypeAndFileNameTooLong(string type)
        {
            _specContext.GivenValidDocument = new DocumentModel
            {
                ContentType = type.ToLower(),
                ContentName = string.Format("dfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTestdfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTestdfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTest.{0}", type.ToLower()),
                Content = new byte[9 * 1024]
            };
        }

    }
}
