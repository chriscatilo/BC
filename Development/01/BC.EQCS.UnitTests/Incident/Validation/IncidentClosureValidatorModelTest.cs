using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Models;
using NSubstitute;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public abstract class IncidentClosureValidatorModelTest : ModelValidatorTest<IncidentClosureModel>
    {
        protected virtual IRepository<RiskRatingModel> Given_Risk_Rating_Repository()
        {
            var repository = Substitute.For<IRepository<RiskRatingModel>>();

            return repository;
        }

        protected override IModelValidator<IncidentClosureModel> Given_Model_Validator()
        {
            return new IncidentClosureModelValidator(Given_Risk_Rating_Repository());
        }
    }
}
