using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentCandidateCountryTest : IncidentCandidateModelValidatorTest
    {
        protected override IncidentCandidateModel Given_Model()
        {
            return new IncidentCandidateModel
            {
                Nationality = "XXX"
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.CountryIsInvalid);
        }
    }
}