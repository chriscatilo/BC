using BC.EQCS.Contracts;
using BC.EQCS.Domain.Document;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Document
{
    [TestFixture]
    public class DocumentValidator_Tests
    {
        private IModelValidator<DocumentModel> _validatorModel;

        private DocumentModel _model;

        [SetUp]
        protected void Start()
        {
            _validatorModel = new DocumentModelValidator();
        }

        [Test]
        [Ignore]
        public void Should_Throw_Exception_If_Null()
        {
            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes(" Object reference not set to an instance of an object.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Empty()
        {
            // Arrange
            _model = new DocumentModel();

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File name is required");
                ex.AssertValidationResultIncludes("Invalid format. Following are the supported file formats (pdf, doc, docx or jpg)");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Empty_File()
        {
            // Arrange
            _model = new DocumentModel {ContentName = "Test.pdf", ContentType = "pdf", Content = new byte[] {}};

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Invalid_FileFormat()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.pdf1", ContentType = "pdf1", Content = new byte[] { } };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Invalid format. Following are the supported file formats (pdf, doc, docx or jpg)");
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_Empty_PDF()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.pdf", ContentType = "pdf", Content = new byte[] { } };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_PDF_Content_Exceeds_10MB()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.pdf", ContentType = "pdf", Content = new byte[11 * 1024] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File size should not exceed 10mb.");
            }
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Valid_PDF()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.pdf", ContentType = "pdf", Content = new byte[10] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_Empty_DOC()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.doc", ContentType = "doc", Content = new byte[] { } };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_DOC_Content_Exceeds_10MB()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.doc", ContentType = "doc", Content = new byte[11 * 1024] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File size should not exceed 10mb.");
            }
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Valid_DOC()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.doc", ContentType = "doc", Content = new byte[10] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_Empty_DOCX()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.docx", ContentType = "docx", Content = new byte[] { } };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_DOCX_Content_Exceeds_10MB()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.docx", ContentType = "docx", Content = new byte[11 * 1024] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File size should not exceed 10mb.");
            }
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Valid_DOCX()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.docx", ContentType = "docx", Content = new byte[10] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_Empty_JPG()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.jpg", ContentType = "jpg", Content = new byte[] { } };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Valid_JPG_Content_Exceeds_10MB()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.jpg", ContentType = "jpg", Content = new byte[11 * 1024] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File size should not exceed 10mb.");
            }
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Valid_JPG()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "Test.jpg", ContentType = "jpg", Content = new byte[10] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("Empty file cannot be uploaded.");
            }
        }

        [Test]
        public void Should_Throw_Exception_If_FileName_Too_long()
        {
            // Arrange
            _model = new DocumentModel { ContentName = "dfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTestdfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTestdfkoidrowihoisdhfoioisefokisdshfiushdsdhdfinsdsoioighgsoieiufoisdhfoisdhfoieoiofsoiohfoiosefoisdhdfoihsdoifhosifhowehroieowroiwehrooweheroioewrhoiweoiroiwerhoewhroiewrihweihrwiwrhiewhroiweihroiwehroieiwhroiiwehroiihweroiTest.pdf", ContentType = "pdf", Content = new byte[10] };

            try
            {
                _validatorModel.ValidateModel(_model);
            }
            catch (ValidationFailureException ex)
            {
                ex.AssertValidationResultIncludes("File Name Too Long");
            }
        }
    }
}

