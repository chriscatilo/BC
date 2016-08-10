using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.IncidentCommandAvailability
{
    public class GetByModelIdTests
    {
        private ICommandAvailabilityManager<IncidentCommand> commandAvailabilityManager;
        private IRepository<IncidentMasterModel> modelRepository;
        private IAspectRepository<IncidentActionModel, IncidentMasterModel> actionModelRepository;
        private ILookup<IncidentStatus, IncidentCommand> statusCommandMapping;
        private IAssetAuthoriser authoriser;
        private int id;
        private IncidentMasterModel incident;
        private IContextResolver contextResolver;

        [SetUp]
        public void Setup()
        {
            id = 1;

            incident = new IncidentMasterModel();

            modelRepository = Substitute.For<IRepository<IncidentMasterModel>>();

            actionModelRepository = Substitute.For<IAspectRepository<IncidentActionModel, IncidentMasterModel>>();
            
            statusCommandMapping = new IncidentAvailableTransitions();

            authoriser  = Substitute.For<IAssetAuthoriser>();

            contextResolver = Substitute.For<IContextResolver>();
            
        }

        private IEnumerable<IncidentCommand> Execute()
        {
            modelRepository.GetById(id).Returns(incident);

            commandAvailabilityManager = new IncidentCommandAvailabilityManager(modelRepository, actionModelRepository, statusCommandMapping, authoriser, contextResolver);

            return commandAvailabilityManager.GetByModelId(id);
        }

        [Test]
        public void IfNoModelIsReturnedFromTheRepositoryThenAnExceptionIsThrown()
        {
            incident = null;

            Assert.Throws<ModelNotFoundException>(() => Execute());
        }

        [Test]
        public void IfTheIncidentIsInProgressAndTheUserIsAuthorisedThenTheUserCanSave()
        {
            incident.Status = IncidentStatus.InProgress;
            
            authoriser.IsAuthorised(Arg.Any<string>()).Returns(true);

            var actual = Execute();

            Assert.Contains(IncidentCommand.Save, actual.ToList());
        }

        [Test]
        public void IfTheIncidentIsNotInProgressAndTheUserIsNotAuthorisedThenTheUserCanNotSave()
        {
            incident.Status = IncidentStatus.InProgress;

            foreach (var incidentStatus in Enum.GetValues(typeof (IncidentStatus)).Cast<IncidentStatus>().Where(x => x != IncidentStatus.None &&  x != IncidentStatus.InProgress))
            {
                incident.Status = incidentStatus;

                authoriser.IsAuthorised(Arg.Any<string>()).Returns(false);

                var actual = Execute();

                Assert.That(actual.ToList(), Has.No.Member(IncidentCommand.Save));
            }
        }
         
    }
}
