using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentRejectionModelValidator : ModelValidator<IncidentRejectionModel>
    {
        public IncidentRejectionModelValidator()
        {
            RuleFor(model => model.Reason).NotEmpty();
        }
    }
}