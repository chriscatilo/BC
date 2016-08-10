using System;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentCandidateValidationBuilderFactory :
        IAspectValidationBuilderFactory<IncidentCandidateModel, IncidentMasterModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly Func<IncidentCandidateModel, IncidentMasterModel, IncidentCandidateValidationBuilder> _createBuilder;
        private readonly IRepository<IncidentCandidateModel> _candidateRepository;

        public IncidentCandidateValidationBuilderFactory(
            IRepository<IncidentCandidateModel> candidateRepository,
            IRepository<CountryModel> countryRepository,
            ISchemaAggregator<IncidentAttributes, IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator)
        {
            _candidateRepository = candidateRepository;
            _createBuilder = (candidate, incident) =>
            {
                var validator = new IncidentCandidateModelValidator(countryRepository);
                return new IncidentCandidateValidationBuilder(validator, candidate, incident, schemaAggregator);
            };
        }

        public IValidationBuilder<IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand> CreateBuilderForNewModel(IncidentMasterModel incident)
        {
            var builder = _createBuilder(null, incident);
            return builder;
        }

        public IValidationBuilder<IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand> CreateBuilder(IncidentMasterModel incident, int modelId)
        {
            var candidate = _candidateRepository.GetById(modelId);

            if (candidate == null)
            {
                throw new ModelNotFoundException();
            }

            var builder = _createBuilder(candidate, incident);
            return builder;
        }
    }
}