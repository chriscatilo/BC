using System;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [TestFixture(IncidentActivityLogType.Rejection)]
    [TestFixture(IncidentActivityLogType.Reopening)]
    public class IncidentActivityLogDateTimeOfActivityIsTomorrowTest : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        public IncidentActivityLogDateTimeOfActivityIsTomorrowTest(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel()
            {
                DateTimeOfActivity = DateTime.Today.AddDays(1),
                LogType = _logType
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.DateTimeOfActivityCannotBeInTheFuture);
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}