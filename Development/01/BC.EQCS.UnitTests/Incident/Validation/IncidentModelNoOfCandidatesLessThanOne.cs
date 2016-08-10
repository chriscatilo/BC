using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelNoOfCandidatesLessThanOne : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                NoOfCandidates = -1
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(
                IncidentValidationErrorMessages.NoOfCandidateLessThanOne);
        }
    }
}