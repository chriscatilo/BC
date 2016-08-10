using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelRefOrgExistsIsEmpty : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReferringOrganisation = "XXXX",
                ReferringOrgExists = null
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.ReferringOrgExistsIsEmpty);
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}