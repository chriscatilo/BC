using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Utils;

namespace BC.EQCS.Web.Controllers.API
{
    public class DocumentController : ApiController
    {
        private readonly IDocumentRepository<DocumentModel, DocumentViewModel> _documentStorageRepository;
        private readonly IModelValidator<DocumentModel> _validatorModel;
        private readonly IContextResolver _contextResolver;

        public DocumentController(IDocumentRepository<DocumentModel, DocumentViewModel> documentStorageRepository, IModelValidator<DocumentModel> validatorModel, IContextResolver contextResolver)
        {
            _documentStorageRepository = documentStorageRepository;
            _validatorModel = validatorModel;
            _contextResolver = contextResolver;
        }

        [HttpPost]
        [Route(ApiRoutes.Document.Route, Name = ApiRoutes.Document.Name)]
        public dynamic Post([FromBody] DocumentModel model)
        {
            try
            {
                // Validate the document model based on rules.....
                _validatorModel.ValidateModel(model);

                var id = _documentStorageRepository.Create(model);
                var uri = Url.GetHrefFromRouteName(ApiRoutes.DocumentById.Name, new { id });

                return this.OkWithLocation(uri);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex);
            }
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentsByActionId.Route, Name = ApiRoutes.DocumentsByActionId.Name)]
        public List<DocumentViewModel> GetActionDocuments(int id)
        {
            return _documentStorageRepository.GetDocumentViewModelsByActionId(id);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentById.Route, Name = ApiRoutes.DocumentById.Name)]
        public DocumentViewModel Get(int id)
        {
            return _documentStorageRepository.GetDocumentViewModelById(id);
        }

        [HttpGet]
        [Route(ApiRoutes.DownloadDocumentById.Route, Name = ApiRoutes.DownloadDocumentById.Name)]
        public HttpResponseMessage Download(int id)
        {
            HttpResponseMessage response = null;
            var document = _documentStorageRepository.GetById(id);

            if (document != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StreamContent(new MemoryStream(document.Content));
                response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                ;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("fileName")
                {
                    FileName = document.ContentName
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }

        [HttpPost]
        [Route(ApiRoutes.IncidentActionByIdDocument.Route, Name = ApiRoutes.IncidentActionByIdDocument.Name)]
        public dynamic Post(int id)
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var uploadedFile = HttpContext.Current.Request.Files[0];
                    var model = new DocumentModel
                    {
                        ContentName = Path.GetFileName(uploadedFile.FileName),
                        ContentType = Path.GetExtension(uploadedFile.FileName).Substring(1),
                        Content = StreamToByte(uploadedFile.InputStream),
                    };

                    // Validate the document model based on rules.....
                    _validatorModel.ValidateModel(model);

                    model.OwnerType = "Action";
                    model.OwnerIdentifier = id;
                    model.UploadedBy = _contextResolver.CurrentUser.Id;

                    var documentId = _documentStorageRepository.Create(model);
                    return _documentStorageRepository.GetDocumentViewModelById(documentId);
                }
                return null;
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex);
            }
        }

        [HttpDelete]
        [Route(ApiRoutes.DeleteDocumentById.Route, Name = ApiRoutes.DeleteDocumentById.Name)]
        public dynamic DeleteDocument(int id)
        {
            _documentStorageRepository.Delete(id);

            return Ok();
        }

        private byte[] StreamToByte(Stream input)
        {
            byte[] buffer = new byte[10 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}