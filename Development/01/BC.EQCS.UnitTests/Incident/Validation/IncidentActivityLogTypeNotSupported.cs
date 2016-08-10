using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [TestFixture(IncidentActivityLogType.None)]
    [TestFixture(IncidentActivityLogType.Change)] // TODO ruleset under development
    [TestFixture(IncidentActivityLogType.ActionUpdated)] // TODO ruleset under development
    public class IncidentActivityLogTypeNotSupported : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _validationRule;

        public IncidentActivityLogTypeNotSupported(IncidentActivityLogType validationRule)
        {
            _validationRule = validationRule;
        }

        protected override bool Given_Validation_Ruleset_Is_Supported()
        {
            // model log type (as validation ruleset) is not supported
            return false;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel
            {
                // log type is picked up by the validator as the validation rule set
                LogType = _validationRule
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            Assert.Fail("Validation did not throw NotSupportedException");
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw NotSupportedException");
        }
    }
}