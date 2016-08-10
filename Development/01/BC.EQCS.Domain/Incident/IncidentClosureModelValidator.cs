using BC.EQCS.Contracts;
using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident
{
    public class IncidentClosureModelValidator : ModelValidator<IncidentClosureModel>
    {
        private readonly IRepository<RiskRatingModel> _riskRatingRepository;

        public IncidentClosureModelValidator(IRepository<RiskRatingModel> riskRatingRepository)
        {
            _riskRatingRepository = riskRatingRepository;

            RuleFor(model => model.Comments)
                .NotEmpty();
        }
    }
}