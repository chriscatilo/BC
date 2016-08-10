using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.UnitTests.Utils;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelTestLocationNotExists : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                TestLocation = Constants.AdminUnitTypes.Root
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.TestLocationDoesNotExist);
        }
    }

    public class IncidentModelTestLocationNotInCentre : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                TestCentre = "GS002",
                TestLocation = "GS001-01"
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(
                IncidentValidationErrorMessages.TestLocationDoesNotBelongToTestCentre);
        }
    }
}