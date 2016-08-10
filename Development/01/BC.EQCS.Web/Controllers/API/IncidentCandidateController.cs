using System;
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
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Web.Infrastructure.Authorisation;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Models.Api;
using BC.EQCS.Web.Utils;
using Elmah;
using Google.ProtocolBuffers;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentCandidateController : ApiController
    {
        private readonly ICommandAvailabilityManager<IncidentCommand> _commandAvailability;
        private readonly IRepository<IncidentMasterModel> _incidentRepository;
        private readonly IAspectRepository<IncidentCandidateModel, IncidentMasterModel> _persistenceRepository;
        private readonly IAspectRepository<IncidentCandidateViewModel, IncidentMasterModel> _viewRepository;
        private readonly IAspectValidationBuilderFactory<IncidentCandidateModel, IncidentMasterModel, IncidentSchemaKeyCriterion, IncidentCommand> _validatorFactory;
        private readonly IActivityLogger<IncidentCandidateViewModel, CandidateAttributeTemplate> _activityLogger;

        public IncidentCandidateController(
            IRepository<IncidentMasterModel> incidentRepository,
            IAspectRepository<IncidentCandidateModel, IncidentMasterModel> persistenceRepository,
            IAspectRepository<IncidentCandidateViewModel, IncidentMasterModel> viewRepository,
            ICommandAvailabilityManager<IncidentCommand> commandAvailability,
            IAspectValidationBuilderFactory<IncidentCandidateModel, IncidentMasterModel, 
            IncidentSchemaKeyCriterion, 
            IncidentCommand> validatorFactory, 
            IActivityLogger<IncidentCandidateViewModel, CandidateAttributeTemplate> activityLogger)
        {
            _incidentRepository = incidentRepository;
            _persistenceRepository = persistenceRepository;
            _viewRepository = viewRepository;
            _commandAvailability = commandAvailability;
            _validatorFactory = validatorFactory;
            _activityLogger = activityLogger;
        }

        [Route(ApiRoutes.IncidentByIdCandidate.Route, Name = ApiRoutes.IncidentByIdCandidate.Name)]
        public dynamic GetForIncident(int id)
        {

            var masterModel = _incidentRepository.GetById(id);
            if (masterModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var addCommandAvailable = _commandAvailability.IsAvailable(id, IncidentCommand.AddCandidate);

            var candidates = _viewRepository
                .GetFor(masterModel)
                .Select(model => CreateCandidateResult(addCommandAvailable, model));

            return candidates;
        }

        [Route(ApiRoutes.IncidentByIdCandidateById.Route, Name = ApiRoutes.IncidentByIdCandidateById.Name)]
        public dynamic Get(int incidentId, int id)
        {
            var model = _viewRepository.GetById(id);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var addCommandAvailable = _commandAvailability.IsAvailable(incidentId, IncidentCommand.AddCandidate);

            var result = CreateCandidateResult(addCommandAvailable, model);

            return result;
        }

        [Route(ApiRoutes.IncidentByIdCandidateByIdPersistence.Route, Name = ApiRoutes.IncidentByIdCandidateByIdPersistence.Name)]
        public dynamic GetPersisted(int incidentId, int id)
        {
            var model = _persistenceRepository.GetById(id);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return model;
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.AddCandidate)]
        [Route(ApiRoutes.IncidentByIdCandidate.Route)]
        public dynamic Post(int id, [FromBody] IncidentCandidateModel candidate)
        {
            var incident = _incidentRepository.GetById(id);
            if (incident == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!_commandAvailability.IsAvailable(id, IncidentCommand.AddCandidate))
            {
                return this.CommandUnavailableResponse(IncidentCommand.AddCandidate, id);
            }
            
            try
            {
                Validate(candidate, incident, IncidentCommand.AddCandidate);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex.ValidationResult, "Incident candidate failed validation");
            }

            candidate.IncidentId = id;
            var candidateId = _persistenceRepository.Create(candidate);


            _activityLogger.LogAllModelValues(candidateId, id, IncidentActivityLogType.NewCandidate);


            var uri = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdCandidateById.Name, new { IncidentId = id, Id = candidateId });

            return this.OkWithLocation(uri);
        }

        [IncidentCommandAuthorisationFilter(IncidentCommand.AddCandidate)]
        [Route(ApiRoutes.IncidentByIdCandidateById.Route)]
        public dynamic Put(int incidentId, int id, [FromBody] IncidentCandidateModel candidate)
        {
            var incident = _incidentRepository.GetById(incidentId);
            if (incident == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!_viewRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Candidate not found");
            }

            if (!_commandAvailability.IsAvailable(incidentId, IncidentCommand.AddCandidate))
            {
                return this.CommandUnavailableResponse(IncidentCommand.AddCandidate, incidentId);
            }

            if (!CheckRaceCondtion(id, candidate))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "RaceConditionConflict");
            }
            
            try
            {
                candidate.Id = id;
                Validate(candidate, incident, IncidentCommand.AddCandidate);
            }
            catch (ValidationFailureException ex)
            {
                return this.FailedValidation(ex.ValidationResult, "Incident candidate failed validation");
            }

            candidate.IncidentId = incidentId;
            candidate.Id = id;

            _persistenceRepository.Update(candidate);

            return Ok();
        }
        
        [IncidentCommandAuthorisationFilter(IncidentCommand.AddCandidate)]
        [Route(ApiRoutes.IncidentByIdCandidateById.Route)]
        public dynamic Delete(int incidentId, int id)
        {
            if (!_incidentRepository.Exists(incidentId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Incident not found");
            }

            if (!_viewRepository.Exists(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Candidate not found");
            }

            if (!_commandAvailability.IsAvailable(incidentId, IncidentCommand.AddCandidate))
            {
                return this.CommandUnavailableResponse(IncidentCommand.AddCandidate, incidentId);
            }

            _persistenceRepository.Delete(id);

            return Ok();
        }

        private CandidateResult CreateCandidateResult(bool addCommandAvailable, IncidentCandidateViewModel model)
        {
            return new CandidateResult
            {
                Uri = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdCandidateById.Name, new { model.IncidentId, model.Id }),

                // if add command is not available then do not expose the candidate's persistence model uri
                Persisted = !addCommandAvailable
                    ? null
                    : Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdCandidateByIdPersistence.Name,
                        new { model.IncidentId, model.Id }),
                Model = model
            };
        }

        private void Validate(IncidentCandidateModel candidate, IncidentMasterModel incident, IncidentCommand command)
        {
            var builder = candidate.Id == 0
                ? _validatorFactory.CreateBuilderForNewModel(incident)
                : _validatorFactory.CreateBuilder(incident,candidate.Id);

            var validator = builder
                .ForEvent(command)
                .Build();

            validator.ValidateModel(candidate);
        }

        /// <summary>
        /// Check the validity of Incident based on rowversion
        /// This resolves any concurrency issues
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckRaceCondtion(int id, IncidentCandidateModel model)
        {
            if (model.RowVersion == null)
            {
                return true;
            }
            var existingModel = _persistenceRepository.GetById(id);
            return model.RowVersion.SequenceEqual(existingModel.RowVersion);
        }
    }
}