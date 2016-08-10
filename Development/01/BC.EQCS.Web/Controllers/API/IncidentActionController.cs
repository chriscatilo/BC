using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Utils;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentActionController : ApiController
    {
        private readonly IActivityLogger<IncidentActionViewModel, ActionAttributeTemplate> _activityLogger;
        private readonly IAssetAuthoriser _authoriser;
        private readonly IDocumentRepository<DocumentModel, DocumentViewModel> _documentStorageRepository;
        private readonly IAspectRepository<IncidentActionModel, IncidentMasterModel> _incidentActionRepository;
        private readonly IModelValidator<IncidentActionModel, IncidentActionRuleSet> _modelTypedValidator;

        public IncidentActionController(
            IAspectRepository<IncidentActionModel, IncidentMasterModel> incidentActionRepository,
            IModelValidator<IncidentActionModel, IncidentActionRuleSet> modelTypedValidator,
            IAssetAuthoriser authoriser,
            IDocumentRepository<DocumentModel, DocumentViewModel> documentStorageRepository,
            IActivityLogger<IncidentActionViewModel, ActionAttributeTemplate> activityLogger)
        {
            _incidentActionRepository = incidentActionRepository;
            _modelTypedValidator = modelTypedValidator;
            _authoriser = authoriser;
            _documentStorageRepository = documentStorageRepository;
            _activityLogger = activityLogger;
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var model = _incidentActionRepository.GetById(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        public IHttpActionResult Post([FromBody] IncidentActionModel model)
        {
            if (!_authoriser.IsAuthorised(AssetType.IncidentUpdateAction, model.IncidentId))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            try
            {
                _modelTypedValidator.ValidateModel(model, IncidentActionRuleSet.CreatingActionRules);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex.ValidationResult, "Incident Action failed validation");
            }

            var id = _incidentActionRepository.Create(model);

            // Update associated orphened Documents OwnerIdentifier
            if (model.DocumentList != null)
            {
                _documentStorageRepository.UpdateOrphenedDocuments(id, model.DocumentList);
            }

            _activityLogger.LogAllModelValues(id, model.IncidentId, IncidentActivityLogType.NewAction);

            var existingModel = _incidentActionRepository.GetById(id);
            return Ok(new { id = id, rowVersion = existingModel.RowVersion });

        }

        [Authorize]
        public dynamic Put(int id, [FromBody] IncidentActionModel model)
        {
            _activityLogger.OpenDifferenceLoggingProcess(id, model.IncidentId);


            //Place holder selector for the validation type
            var response = !string.IsNullOrEmpty(model.ActionResponse);

            try
            {
                if (response)
                    _modelTypedValidator.ValidateModel(model, IncidentActionRuleSet.RespondingToActionAndClosingRules);

                _modelTypedValidator.ValidateModel(model, IncidentActionRuleSet.EditingActionRules);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

            var incidentAction = _incidentActionRepository.GetById(id);

            if (incidentAction == null)
            {
                return BadRequest("Incident Action with id " + id + " not found");
            }

            if (!CheckRaceCondtion(id, model))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
            }

            if (!_authoriser.IsAuthorised(AssetType.IncidentUpdateAction, incidentAction.IncidentId) && !_authoriser.IsAuthorised(AssetType.IncidentRespondAction, incidentAction.IncidentId))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            try
            {
                _incidentActionRepository.Update(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            _activityLogger.CompleteDifferenceLoggingProcess(IncidentActivityLogType.ActionUpdated);


            return Ok();
        }

        /// <summary>
        /// Check the validity of Incident Action based on rowversion
        /// This resolves any concurrency issues
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckRaceCondtion(int id, IncidentActionModel model)
        {
            if (model.RowVersion == null)
            {
                return false;
            }
            var existingModel = _incidentActionRepository.GetById(id);
            return model.RowVersion.SequenceEqual(existingModel.RowVersion);
        }
    }
}