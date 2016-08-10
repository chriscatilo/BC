using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Domain.Incident.Schema
{
    public class IncidentSchemaBuildDirector : ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion>
    {
        private readonly ICommandAvailabilityManager<IncidentCommand> _commandAvailabilityManager;
        private readonly IContextResolver _contextResolver;
        private readonly ISchemaBuilderFactory<IncidentAttributes, IncidentStatus, IncidentCommand> _builderFactory;
        private readonly IRepository<IncidentMasterModel> _modelRepository;
        private readonly ISchemaKeyRepository<IncidentSchemaKeyCriterion> _schemaKeyRepository;

        public IncidentSchemaBuildDirector(
            ISchemaKeyRepository<IncidentSchemaKeyCriterion> schemaKeyRepository,
            ICommandAvailabilityManager<IncidentCommand> commandAvailabilityManager,
            IContextResolver contextResolver,
            ISchemaBuilderFactory<IncidentAttributes, IncidentStatus, IncidentCommand> builderFactory, 
            IRepository<IncidentMasterModel> modelRepository)
        {
            _schemaKeyRepository = schemaKeyRepository;
            _commandAvailabilityManager = commandAvailabilityManager;
            _contextResolver = contextResolver;
            _builderFactory = builderFactory;
            _modelRepository = modelRepository;
        }

        public IEnumerable<NamedSchema<IncidentAttributes>> GetSchemataForNewModel(IncidentSchemaKeyCriterion criterion)
        {
            var availableCommands = _commandAvailabilityManager.GetForNewModel().ToArray();

            var applicationRoles = _contextResolver
                .CurrentUser
                .ApplicationRoles
                .Select(role => role.ShortCode);

            var schemaKey = _schemaKeyRepository.Get(applicationRoles, criterion);

            var result = _builderFactory
                .CreateBuilderByKey(schemaKey)
                .IncludeAugmentsFor(availableCommands)
                .Build();

            return result;
        }

        public IEnumerable<NamedSchema<IncidentAttributes>> GetSchemata(int modelId, IncidentSchemaKeyCriterion criterion)
        {
            var model = _modelRepository.GetById(modelId);

            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            var availableCommands = _commandAvailabilityManager.GetByModelId(modelId).ToArray();

            var applicationRoles = _contextResolver
                .CurrentUser
                .ApplicationRoles
                .Select(role => role.ShortCode);

            criterion.IncidentClass = criterion.IncidentClass ?? model.IncidentClass;

            var schemaKey = _schemaKeyRepository.Get(applicationRoles, criterion);

            var result = _builderFactory
                .CreateBuilderByKey(schemaKey)
                .ForStatus(model.Status)
                .IncludeAugmentsFor(availableCommands)
                .Build();

            return result;
        }
    }
}