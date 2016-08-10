using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public abstract class IncidentRejectionModelValidatorTest : ModelValidatorTest<IncidentRejectionModel>
    {
        protected override IModelValidator<IncidentRejectionModel> Given_Model_Validator()
        {
            var validator = new IncidentRejectionModelValidator();

            return validator;
        }
    }
}