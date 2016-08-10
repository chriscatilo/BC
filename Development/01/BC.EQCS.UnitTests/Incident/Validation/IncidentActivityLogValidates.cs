using System;
using System.Threading.Tasks;
using BC.EQCS.Contracts;
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
    public class IncidentActivityLogValidates : IncidentActivityLogModelValidatorTest
    {
        private readonly IncidentActivityLogType _logType;

        private const int IncidentId = 123;

        public IncidentActivityLogValidates(IncidentActivityLogType logType)
        {
            _logType = logType;
        }

        protected override IRepository<IncidentModel> Given_Incident_Repository()
        {
            var mockedRepository = Substitute.For<IRepository<IncidentModel>>();

            mockedRepository.Exists(Arg.Any<int>()).Returns(true);

            return mockedRepository;
        }

        protected override IAsyncRepository<SecurityUserModel> Given_User_Repository()
        {
            var userRepository = Substitute.For<IAsyncRepository<SecurityUserModel>>();

            userRepository.Exists(Arg.Any<string>()).Returns(Task.FromResult(true));

            return userRepository;
        }

        protected override IncidentActivityLogModel Given_Model()
        {
            return new IncidentActivityLogModel()
            {
                IncidentId = IncidentId,
                DateTimeOfActivity = DateTime.Now,
                Payload = new RandomValueGenerators.ParagraphGenerator().Generate(),
                LogType = _logType,
                User = new UserModel { ObjectGuid = Guid.NewGuid() }
            };
        }
        
        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}