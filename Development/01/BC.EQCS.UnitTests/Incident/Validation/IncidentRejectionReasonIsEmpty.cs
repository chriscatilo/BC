using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentRejectionReasonIsEmpty : IncidentRejectionModelValidatorTest
    {
        protected override IncidentRejectionModel Given_Model()
        {
            return new IncidentRejectionModel {Reason = string.Empty};
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes("'Reason' should not be empty.");
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}