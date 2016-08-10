using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Models;
using NSubstitute;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public abstract class IncidentActivityLogModelValidatorTest : ModelValidatorTest<IncidentActivityLogModel, IncidentActivityLogType>
    {
        protected virtual IRepository<IncidentModel> Given_Incident_Repository()
        {
            return Substitute.For<IRepository<IncidentModel>>();
        }

        protected virtual IAsyncRepository<SecurityUserModel> Given_User_Repository()
        {
            var userRepository = Substitute.For<IAsyncRepository<SecurityUserModel>>();

            return userRepository;
        }

        protected override IModelValidator<IncidentActivityLogModel, IncidentActivityLogType> Given_Model_Validator()
        {
            var incidentRepository = Given_Incident_Repository();

            var userRepository = Given_User_Repository();

            var validator = new IncidentActivityLogModelValidator(incidentRepository, userRepository);

            return validator;
        }

        protected override IncidentActivityLogType Given_Validation_Ruleset()
        {
            var model = Given_Model();

            // the log type is the validation ruleset
            return model.LogType;
        }
    }
}