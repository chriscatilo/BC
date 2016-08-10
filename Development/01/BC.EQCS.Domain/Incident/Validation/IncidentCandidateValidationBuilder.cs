using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentCandidateValidationBuilder :
        IValidationBuilder<IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly IncidentCandidateModel _candidateModel;
        private readonly IncidentMasterModel _incidentModel;

        private readonly
            ISchemaAggregator<IncidentAttributes, IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand>
            _schemaAggregator;

        private readonly ModelValidator<IncidentCandidateModel> _validator;
        private IncidentCommand? _command;
        private IncidentSchemaKeyCriterion _keyCriterion;

        public IncidentCandidateValidationBuilder(
            ModelValidator<IncidentCandidateModel> validator,
            IncidentCandidateModel candidateModel,
            IncidentMasterModel incidentModel,
            ISchemaAggregator<IncidentAttributes, IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator)
        {
            _validator = validator;
            _candidateModel = candidateModel;
            _incidentModel = incidentModel;
            _schemaAggregator = schemaAggregator;
        }

        public IValidationBuilder<IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand> ForCriterion(
            IncidentSchemaKeyCriterion keyCriterion)
        {
            _keyCriterion = keyCriterion;
            return this;
        }

        public IValidationBuilder<IncidentCandidateModel, IncidentSchemaKeyCriterion, IncidentCommand> ForEvent(
            IncidentCommand command)
        {
            _command = command;
            return this;
        }

        public IModelValidator<IncidentCandidateModel> Build()
        {
            _keyCriterion = _keyCriterion ?? new IncidentSchemaKeyCriterion
            {
                IncidentClass = _incidentModel.IncidentClass
            };

            var schemaAggregator = _schemaAggregator.ForCriterion(_keyCriterion);
            if (_command != null)
            {
                schemaAggregator.ForEvent(_command ?? IncidentCommand.None);
            }

            var join = schemaAggregator.Aggregate(_incidentModel.Id);

            // TODO Chris: nulls should be treated as Not Applicable
            foreach (var item in join.Where(item => item.SchemaMember != null))
            {
                switch (item.SchemaMember.Constraint)
                {
                    case ValueConstraint.Mandatory:
                        _validator.RuleFor(item.TargetMember)
                            .NotEmpty()
                            .WithMessage(string.Format("'{0}' should not be empty.", item.SchemaMember.Label));
                        break;

                    case ValueConstraint.NotApplicable:
                        _validator.RuleFor(item.TargetMember)
                            .IsNull()
                            .WithMessage(string.Format("'{0}' cannot be set.", item.SchemaMember.Label));
                        break;

                    case ValueConstraint.ViewOnly:
                        _validator.RuleFor(item.TargetMember)
                            .MustBeEqual(_candidateModel, item.TargetMember.Compile())
                            .WithMessage(string.Format("'{0}' cannot be set or altered.", item.SchemaMember.Label));
                        break;
                }
            }

            return _validator;
        }
    }
}