using System;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentValidationBuilderFactory :
        IValidationBuilderFactory<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly Func<IncidentModel, IncidentValidationBuilder> _createBuilder;
        private readonly IRepository<IncidentModel> _incidentRepository;

        public IncidentValidationBuilderFactory(
            IRepository<IncidentModel> incidentRepository,
            IRepository<RiskRatingModel> riskRatingRepository,
            IRepository<ResidualRiskRatingModel> residualRiskRatingRepository,
            IRepository<OrganisationTypeModel> orgTypeRepository,
            IRepository<UkviImmediateReportTypeModel> ukviImmediateReportTypeRepository,
            ITreeRepository<IncidentClassModel> incidentClassRepository, 
            IUserContext userContext,
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator)
        {
            _incidentRepository = incidentRepository;
            _createBuilder = model =>
            {
                var validator = new IncidentModelValidator(
                    riskRatingRepository,
                    residualRiskRatingRepository,
                    orgTypeRepository,
                    incidentClassRepository,
                    ukviImmediateReportTypeRepository,
                    userContext);
                return new IncidentValidationBuilder(validator, model, schemaAggregator);
            };
        }

        public IValidationBuilder<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> CreateBuilderForNewModel()
        {
            var builder = _createBuilder(null);
            return builder;
        }

        public IValidationBuilder<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> CreateBuilder(int modelId)
        {
            var model = _incidentRepository.GetById(modelId);

            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            var builder = _createBuilder(model);
            return builder;
        }
    }
}