using System;
using System.Net.Http;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Document
{
    public partial class DocumentSpecFlowContextWrapper : SpecFlowContextWrapper
    {
        public DocumentModel GivenValidDocument
        {
            get { return (DocumentModel)ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocument]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocument] = value; }
        }

        public DocumentModel CreateValidDocument()
        {
            var str = "this is file content";
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return new DocumentModel
            {
                ContentName = "Problems faced during Code Build.docx",
                ContentType = "docx",
                Content = bytes
            };
        }

        public DocumentModel GivenInValidDocument
        {
            get { return (DocumentModel)ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocument]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocument] = value; }
        }

        public DocumentModel CreateInValidDocument()
        {
            return new DocumentModel
            {
                ContentName = "Test.pdf1",
                ContentType = "pdf1",
                Content = new byte[11 * 1024 * 1024]
            };
        }

        public DocumentModel GivenValidDocumentExceeds10MB
        {
            get { return (DocumentModel)ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocumentExceeds10MB]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenValidDocumentExceeds10MB] = value; }
        }

        public DocumentModel CreateValidDocumentExceeds10MB()
        {
            return new DocumentModel
            {
                ContentName = "Problems faced during Code Build.docx",
                ContentType = "docx",
                Content = new byte[11 * 1024 * 1024]
            };
        }

        public bool Success
        {
            get { return (bool)ScenarioContext.Current[Constants.FeatureKeys.Success]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.Success] = value; }
        }

        public bool Failure
        {
            get { return (bool)ScenarioContext.Current[Constants.FeatureKeys.Failure]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.Failure] = value; }
        }

        public ValidationFailureException ValidationFailureException
        {
            get { return (ValidationFailureException)ScenarioContext.Current[Constants.FeatureKeys.ValidationFailureException]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ValidationFailureException] = value; }
        }

        public Uri Location
        {
            get { return (Uri)ScenarioContext.Current[Constants.FeatureKeys.Location]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.Location] = value; }
        }

        public DocumentModel GivenValidEmptyDocument
        {
            get { return (DocumentModel)ScenarioContext.Current[Constants.FeatureKeys.GivenValidEmptyDocument]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.GivenValidEmptyDocument] = value; }
        }

        public void ValidResponse(HttpResponseMessage response)
        {
            Location = response.Headers.Location;
            Success = true;
        }

        public void InValidResponse(object result)
        {
            ValidationFailureException = new ValidationFailureException((ValidationResult)
                JsonConvert.DeserializeObject(
                    ((JContainer) (result)).Last.First.ToString(), typeof (ValidationResult)));

            Failure = true;
        }

        public void GetDocumentModel(HttpResponseMessage response)
        {
            ResultantDocumentModel = response.Content.ReadAsAsync<DocumentModel>().Result;
        }

        public DocumentModel ResultantDocumentModel
        {
            get { return (DocumentModel)ScenarioContext.Current[Constants.FeatureKeys.ResultantDocumentModel]; }

            set { ScenarioContext.Current[Constants.FeatureKeys.ResultantDocumentModel] = value; }
        }
    }
}
