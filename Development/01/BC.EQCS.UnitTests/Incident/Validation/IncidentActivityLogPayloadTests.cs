using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [TestFixture(IncidentActivityLogType.Rejection)]
    [TestFixture(IncidentActivityLogType.Reopening)]
    public class IncidentActivityLogPayloadIsEmptyTests : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        public IncidentActivityLogPayloadIsEmptyTests(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel()
            {
                Payload = string.Empty,
                LogType = _logType
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes("'Payload' should not be empty.");
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}