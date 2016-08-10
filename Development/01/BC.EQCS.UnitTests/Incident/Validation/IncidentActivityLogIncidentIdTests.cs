using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [TestFixture(IncidentActivityLogType.Rejection)]
    [TestFixture(IncidentActivityLogType.Reopening)]
    public class IncidentActivityLogIncidentNotExists : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        public IncidentActivityLogIncidentNotExists(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        private const int IncidentId = 123;

        protected override IRepository<IncidentModel> Given_Incident_Repository()
        {
            var mockedRepository = Substitute.For<IRepository<IncidentModel>>();

            mockedRepository.Exists(Arg.Any<int>()).Returns(false);

            return mockedRepository;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel()
            {
                IncidentId = IncidentId,
                LogType = _logType
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.IncidentDoesNotExist);
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}