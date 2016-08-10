using System;
using System.Threading.Tasks;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Models;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [TestFixture(IncidentActivityLogType.Rejection)]
    [TestFixture(IncidentActivityLogType.Reopening)]
    public class IncidentActivityLogUserIsNull : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        public IncidentActivityLogUserIsNull(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel
            {
                User = null,
                LogType = _logType
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes("'User' should not be empty.");
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }

    [TestFixture(IncidentActivityLogType.Rejection)]
    [TestFixture(IncidentActivityLogType.Reopening)]
    public class IncidentActivityLogUserDoesNotExist : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        public IncidentActivityLogUserDoesNotExist(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        protected override IAsyncRepository<SecurityUserModel> Given_User_Repository()
        {
            var userRepository = Substitute.For<IAsyncRepository<SecurityUserModel>>();

            // exist query always returns false, regardless of user identifier
            userRepository.Exists(Arg.Any<string>()).Returns(Task.FromResult(false));

            return userRepository;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel
            {
                User = new UserModel {ObjectGuid = Guid.NewGuid()},
                LogType = _logType
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.UserDoesNotExist);
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}