using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentClosureModelCommentsIsEmpty : IncidentClosureValidatorModelTest
    {
        protected override IncidentClosureModel Given_Model()
        {
            return new IncidentClosureModel() {Comments = string.Empty};
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes("'Comments' should not be empty.");
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}