using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentRejectionValidates : IncidentRejectionModelValidatorTest
    {
        protected override IncidentRejectionModel Given_Model()
        {
            return new IncidentRejectionModel()
            {
                Reason = new RandomValueGenerators.ParagraphGenerator().Generate()
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertFailureDueToException();
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}