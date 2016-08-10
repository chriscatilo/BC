using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC.EQCS.ActivityLog.Logger;
using BC.EQCS.ActivityLog.Logger.AttributeTemplates;
using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.ModelUpdater;
using BC.EQCS.Domain.Security;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Infrastructure.Authorisation;
using BC.EQCS.Web.Infrastructure.Logging;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Models.Api;
using BC.EQCS.Web.Utils;
using BC.EQCS.Workflow;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentController : ApiController
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly ICommandAvailabilityManager<IncidentCommand> _commandAvailability;
        private readonly IRepository<IncidentMasterModel> _incidentMasterRepository;
        private readonly IRepository<IncidentModel> _incidentRepository;
        private readonly IIncidentTabAvailablityRetriever _incidentTabAvailablityRetriever;
        private readonly IRepository<IncidentViewModel> _incidentViewRepository;
        private readonly IActivityLogger<IncidentViewModel, IncidentAttributeTemplate> _activityLogger;

        private readonly INotificationRepository<NotificationMessageModel, NotificationMessageTemplateModel> _notificationRepository;
        private readonly INotificationTemplateRepository<NotificationMessageTemplateModel> _notificationTemplateRepository;
        private readonly IContextResolver _context;
        private readonly ILogger _logger;


        private readonly
            IModelUpdaterFactory
                <IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand, IncidentModelUpdateStrategyKey>
            _updaterFactory;

        private readonly IValidationBuilderFactory<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
            _validatorFactory;

        private readonly IWorkflowFactory _workflowFactory;

        public IncidentController(
            IRepository<IncidentModel> incidentRepository,
            IRepository<IncidentMasterModel> incidentMasterRepository,
            IRepository<IncidentViewModel> incidentViewRepository,
            IWorkflowFactory workflowFactory,
            ICommandAvailabilityManager<IncidentCommand> commandAvailabilityManager,
            IValidationBuilderFactory<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> validatorFactory,
            IModelUpdaterFactory
                <IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand, IncidentModelUpdateStrategyKey>
                updaterFactory,
            IIncidentTabAvailablityRetriever incidentTabAvailablityRetriever,
            IAssetAuthoriser authoriser,
            IActivityLogger<IncidentViewModel, IncidentAttributeTemplate> activityLogger,
            INotificationRepository<NotificationMessageModel, NotificationMessageTemplateModel> notificationRepository,
            INotificationTemplateRepository<NotificationMessageTemplateModel> notificationTemplateRepository,
            IContextResolver context,
            ILogger logger)
        {
            _incidentRepository = incidentRepository;
            _workflowFactory = workflowFactory;
            _commandAvailability = commandAvailabilityManager;
            _validatorFactory = validatorFactory;
            _updaterFactory = updaterFactory;
            _incidentTabAvailablityRetriever = incidentTabAvailablityRetriever;
            _authoriser = authoriser;
            _incidentMasterRepository = incidentMasterRepository;
            _incidentViewRepository = incidentViewRepository;
            _activityLogger = activityLogger;
            _notificationRepository = notificationRepository;
            _notificationTemplateRepository = notificationTemplateRepository;
            _context = context;
            _logger = logger;

          
        }

        [Route(ApiRoutes.IncidentById.Route, Name = ApiRoutes.IncidentById.Name)]
        public dynamic Get(int id)
        {
            var model = _incidentViewRepository.GetById(id);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!IsAuthorised(id))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var availableCommands = _commandAvailability.GetByModelId(id);

            var result = new GetIncidentResult
            {
                Uri = Url.GetHrefFromRouteName(ApiRoutes.IncidentById.Name, new { id }),
                Candidates = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdCandidate.Name, new { id }),
                Model = model,
                Resource = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdResource.Name, new { id }),
                Persisted = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdPersistence.Name, new { id }),
                Commands = availableCommands.CreateAvailableCommandLinks(Url, id),
                TabsAvailable = _incidentTabAvailablityRetriever.Get(id)
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private bool IsAuthorised(int id)
        {
            return _authoriser.IsAuthorised(AssetType.IncidentModuleAccess, id);
        }

        [Route(ApiRoutes.IncidentByIdPersistence.Route, Name = ApiRoutes.IncidentByIdPersistence.Name)]
        public dynamic GetPersisted(int id)
        {
            var model = _incidentRepository.GetById(id);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!IsAuthorised(id))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            var modelToPresent
                = _updaterFactory.CreateUpdater(IncidentModelUpdateStrategyKey.ForPresentation)
                    .ForCriterion(schemaCriterion)
                    .Update(id, model);

            return Request.CreateResponse(HttpStatusCode.OK, modelToPresent);
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Save)]
        [Route(ApiRoutes.Incident.Route, Name = ApiRoutes.Incident.Name)]
        public dynamic Post([FromBody] IncidentModel model)
        {
            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            var incidentToPersist = _updaterFactory
                .CreateUpdater
                (
                    IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForDefaultRiskRating,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                )
                .ForCriterion(schemaCriterion)
                .ForEvent(IncidentCommand.Save)
                .Update(model);

            try
            {
                Validate(incidentToPersist, schemaCriterion, IncidentCommand.Save);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex.ValidationResult, "Incident failed validation");
            }

            var id = _incidentRepository.Create(incidentToPersist);

            var uri = Url.GetHrefFromRouteName(ApiRoutes.IncidentById.Name, new { id });

            return this.OkWithLocation(uri);
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Save)]
        [Route(ApiRoutes.IncidentById.Route)]
        public dynamic Put(int id, [FromBody] IncidentModel model)
        {
            var existingModel = _incidentMasterRepository.GetById(id);

            if (existingModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (existingModel.Status >= IncidentStatus.Submitted)
            {
                _activityLogger.OpenDifferenceLoggingProcess(id);
            }

            const IncidentCommand command = IncidentCommand.Save;

            if (!_commandAvailability.IsAvailable(id, command))
            {
                return this.CommandUnavailableResponse(command, id);
            }

            if (!CheckRaceCondtion(id, model))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
            }

            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            var modelToPersist = _updaterFactory
                .CreateUpdater
                (
                    IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                )
                .ForCriterion(schemaCriterion)
                .ForEvent(command)
                .Update(id, model);

            try
            {
                model.Id = id;
                Validate(modelToPersist, schemaCriterion, command);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

            _incidentRepository.Update(modelToPersist);

            if (existingModel.Status >= IncidentStatus.Submitted)
            {
                _activityLogger.CompleteDifferenceLoggingProcess(IncidentActivityLogType.Change);
            }

            return Ok();
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Raise)]
        [Route(ApiRoutes.IncidentSubmission.Route, Name = ApiRoutes.IncidentSubmission.Name)]
        public dynamic PostSubmission([FromBody] IncidentModel model)
        {
            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            var incidentToPersist = _updaterFactory
                .CreateUpdater
                (
                    IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForDefaultRiskRating,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                )
                .ForCriterion(schemaCriterion)
                .ForEvent(IncidentCommand.Raise)
                .Update(model);

            try
            {
                Validate(incidentToPersist, schemaCriterion, IncidentCommand.Raise);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

            incidentToPersist.Id = _incidentRepository.Create(incidentToPersist);

            var workflow = _workflowFactory.Create<IncidentGenericWorkflowModel>();

            workflow.Execute(incidentToPersist.Id,
                new IncidentGenericWorkflowModel(IncidentStatus.Submitted, IncidentActivityLogType.Submission));

            var uri = Url.GetHrefFromRouteName(ApiRoutes.IncidentById.Name, new { incidentToPersist.Id });

            _activityLogger.LogAllModelValues(incidentToPersist.Id, IncidentActivityLogType.Submission);
            
            new NotificationController(
            _notificationRepository,
            _notificationTemplateRepository,
            _incidentViewRepository,
            _authoriser,
            _context,
            _activityLogger,
            _logger).SendRaisedIncidentNotification(incidentToPersist.Id);
            
             return this.OkWithLocation(uri);
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Raise)]
        [Route(ApiRoutes.IncidentByIdSubmission.Route, Name = ApiRoutes.IncidentByIdSubmission.Name)]
        public dynamic PutSubmission(int id, [FromBody] IncidentModel model)
        {
            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            const IncidentCommand command = IncidentCommand.Raise;

            if (!_commandAvailability.IsAvailable(id, command))
            {
                return this.CommandUnavailableResponse(command, id);
            }

            if (!CheckRaceCondtion(id, model))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
            }

            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            var modelToPersist = _updaterFactory.CreateUpdater
                (
                    IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForDefaultRiskRating,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                )
                .ForCriterion(schemaCriterion)
                .ForEvent(command)
                .Update(model);

            try
            {
                modelToPersist.Id = id;
                Validate(modelToPersist, schemaCriterion, command);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

            _incidentRepository.Update(modelToPersist);


            var workflow = _workflowFactory.Create<IncidentGenericWorkflowModel>();

            workflow.Execute(modelToPersist.Id,
                new IncidentGenericWorkflowModel(IncidentStatus.Submitted, IncidentActivityLogType.Submission));

            _activityLogger.LogAllModelValues(modelToPersist.Id, IncidentActivityLogType.Submission);

            return Ok();
        }

        [HttpDelete, IncidentCommandAuthorisationFilter(IncidentCommand.Delete)]
        [Route(ApiRoutes.IncidentById.Route)]
        public dynamic Delete(int id)
        {
            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!_commandAvailability.IsAvailable(id, IncidentCommand.Delete))
            {
                return this.CommandUnavailableResponse(IncidentCommand.Delete, id);
            }

            _incidentRepository.Delete(id);
            

            return Ok();
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Accept)]
        [Route(ApiRoutes.IncidentByIdAcceptance.Route, Name = ApiRoutes.IncidentByIdAcceptance.Name)]
        public dynamic PutAcceptance(int id, [FromBody] IncidentModel model)
        {
            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            const IncidentCommand command = IncidentCommand.Accept;

            if (!_commandAvailability.IsAvailable(id, command))
            {
                return this.CommandUnavailableResponse(IncidentCommand.Delete, id);
            }

            if (model != null)
            {
                if (!CheckRaceCondtion(id, model))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
                }
            }

            var persistUpdate = model != null;

            // if no model was passed, then get what is already persisted
            model = model ?? _incidentRepository.GetById(id);

            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = model.SubCategory ?? model.Category
            };

            model = _updaterFactory
                .CreateUpdater
                (
                    IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                )
                .ForCriterion(schemaCriterion)
                .ForEvent(command)
                .Update(id, model);

            try
            {
                model.Id = id;
                Validate(model, schemaCriterion, command);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

            // only persist the incident update if model was passed
            if (persistUpdate)
            {
                _incidentRepository.Update(model);
            }

            var workflow = _workflowFactory.Create<IncidentGenericWorkflowModel>();
            workflow.Execute(id,
                new IncidentGenericWorkflowModel(IncidentStatus.InProgress, IncidentActivityLogType.Acceptance));

            _activityLogger.LogAllModelValues(model.Id, IncidentActivityLogType.IncidentSnapshot);

            return Ok();
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Reject)]
        [Route(ApiRoutes.IncidentByIdRejection.Route, Name = ApiRoutes.IncidentByIdRejection.Name)]
        public dynamic PutRejection(int id, [FromBody] IncidentRejectionModel model)
        {
            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!_commandAvailability.IsAvailable(id, IncidentCommand.Reject))
            {
                return this.CommandUnavailableResponse(IncidentCommand.Delete, id);
            }

            var workflow = _workflowFactory.Create<IncidentRejectionModel>();

            try
            {
                workflow.Execute(id, model);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex);
            }

            return Ok();
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.Close)]
        [Route(ApiRoutes.IncidentByIdClosure.Route, Name = ApiRoutes.IncidentByIdClosure.Name)]
        public dynamic PutClosure(int id, [FromBody] IncidentAndWorkflowPayload<IncidentClosureModel> payload)
        {
            _activityLogger.OpenDifferenceLoggingProcess(id);

            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            const IncidentCommand command = IncidentCommand.Close;

            if (!_commandAvailability.IsAvailable(id, command))
            {
                return this.CommandUnavailableResponse(command, id);
            }

            if (payload.IncidentModel != null)
            {
                if (!CheckRaceCondtion(id, payload.IncidentModel))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
                }
            }

            // if no model was passed, then validate what is already persistedpayload.IncidentModel = payload.IncidentModel ?? _incidentRepository.GetById(id);
            payload.IncidentModel = payload.IncidentModel ?? _incidentRepository.GetById(id);

            var schemaCriterion = new IncidentSchemaKeyCriterion
            {
                IncidentClass = payload.IncidentModel.SubCategory ?? payload.IncidentModel.Category
            };

                var modelToPersist = _updaterFactory
                    .CreateUpdater
                    (
                        IncidentModelUpdateStrategyKey.ForPersistence,
                    IncidentModelUpdateStrategyKey.ForNullifySubCategory,
                    IncidentModelUpdateStrategyKey.ForResolveUkvi
                    )
                    .ForCriterion(schemaCriterion)
                    .ForEvent(command)
                    .Update(id, payload.IncidentModel);

            try
            {
                Validate(modelToPersist, schemaCriterion, command);
            }
            catch (ValidationFailureException vfe)
            {
                return this.FailedValidation(vfe.ValidationResult, "Incident failed validation");
            }

                _incidentRepository.Update(modelToPersist);


            var workflow = _workflowFactory.Create<IncidentClosureModel>();

            try
            {
                workflow.Execute(id, payload.WorkflowModel);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex);
            }

            _activityLogger.CompleteDifferenceLoggingProcess(IncidentActivityLogType.Change);

            return Ok();
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.ReOpen)]
        [Route(ApiRoutes.IncidentByIdReopening.Route, Name = ApiRoutes.IncidentByIdReopening.Name)]
        public dynamic PutReopening(int id, [FromBody] IncidentReopeningModel model)
        {
            if (!_incidentRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!_commandAvailability.IsAvailable(id, IncidentCommand.ReOpen))
            {
                return this.CommandUnavailableResponse(IncidentCommand.ReOpen, id);
            }

            var workflow = _workflowFactory.Create<IncidentReopeningModel>();

            try
            {
                workflow.Execute(id, model);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex);
            }

            return Ok();
        }

        private void Validate(
            IncidentModel model,
            IncidentSchemaKeyCriterion schemaCriterion,
            IncidentCommand command)
        {
            var builder = model.Id == 0
                ? _validatorFactory.CreateBuilderForNewModel()
                : _validatorFactory.CreateBuilder(model.Id);

            var validator = builder
                .ForCriterion(schemaCriterion)
                .ForEvent(command)
                .Build();

            validator.ValidateModel(model);
        }

        /// <summary>
        /// Check the validity of Incident based on rowversion
        /// This resolves any concurrency issues
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckRaceCondtion(int id, IncidentModel model)
        {
            if (model.RowVersion == null)
            {
                return true;
            }
            var existingModel = _incidentMasterRepository.GetById(id);
            return model.RowVersion.SequenceEqual(existingModel.RowVersion);
        }
    }
}