using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentReopeningModelValidator : ModelValidator<IncidentReopeningModel>
    {
        public IncidentReopeningModelValidator()
        {
            RuleFor(model => model.Reason).NotEmpty();
        }
    }
}