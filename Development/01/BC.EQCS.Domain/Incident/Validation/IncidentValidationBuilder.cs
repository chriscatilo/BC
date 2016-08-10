using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentValidationBuilder :
        IValidationBuilder<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly IncidentModel _incidentModel;

        private readonly
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
            _schemaAggregator;

        private readonly ModelValidator<IncidentModel> _validator;
        private IncidentCommand? _command;
        private IncidentSchemaKeyCriterion _keyCriterion;

        public IncidentValidationBuilder(
            ModelValidator<IncidentModel> validator,
            IncidentModel incidentModel,
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator)
        {
            _validator = validator;
            _incidentModel = incidentModel;
            _schemaAggregator = schemaAggregator;
        }

        public IValidationBuilder<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> ForCriterion(
            IncidentSchemaKeyCriterion keyCriterion)
        {
            _keyCriterion = keyCriterion;
            return this;
        }

        public IValidationBuilder<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> ForEvent(
            IncidentCommand command)
        {
            _command = command;
            return this;
        }

        public IModelValidator<IncidentModel> Build()
        {
            var schemaAggregator = _schemaAggregator.ForCriterion(_keyCriterion);
            if (_command != null)
            {
                schemaAggregator.ForEvent(_command ?? IncidentCommand.None);
            }

            var join = _incidentModel == null || _incidentModel.Id == 0
                ? schemaAggregator.AggregateForNewModel()
                : schemaAggregator.Aggregate(_incidentModel.Id);


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
                            .MustBeEqual(_incidentModel, item.TargetMember.Compile())
                            .WithMessage(string.Format("'{0}' cannot be set or altered.", item.SchemaMember.Label));
                        break;
                }
            }

            return _validator;
        }
    }
}