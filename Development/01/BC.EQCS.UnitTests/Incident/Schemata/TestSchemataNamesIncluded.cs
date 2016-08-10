using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    // Test fixtures are in nested files
    [TestFixture]
    public partial class TestSchemataNamesIncluded
    {
        private readonly string _assetKey;
        private readonly IncidentStatus? _currentStatus;
        private readonly IEnumerable<string> _expectedSchemaNames;
        private IEnumerable<NamedSchema<IncidentAttributes>> _schemataUnderTest;

        public TestSchemataNamesIncluded(string assetKey, IncidentStatus? currentStatus,
            params dynamic[] expectedSchemaNames)
        {
            _assetKey = assetKey;
            _currentStatus = currentStatus;
            _expectedSchemaNames = expectedSchemaNames.Select(i => i.ToString()).Cast<string>();
        }

        [TestFixtureSetUp]
        public void When_Incident_Model_Schema_Is_Created()
        {
            // given all available commands
            var availableCommands = Enum.GetValues(typeof(IncidentCommand)).Cast<IncidentCommand>().ToArray();

            // when schemata is built
            var availableTransitions = new IncidentAvailableTransitions();
            var builderDirector = new IncidentSchemaFactory(availableTransitions);
            var schemataBuilder = builderDirector.CreateBuilderByKey(_assetKey);
            if (_currentStatus != null)
            {
                schemataBuilder = schemataBuilder.ForStatus((IncidentStatus) _currentStatus);
            }
            if (availableCommands.Any())
            {
                schemataBuilder = schemataBuilder.IncludeAugmentsFor(availableCommands);
            }
            _schemataUnderTest = schemataBuilder.Build().ToList();
        }

        [Test]
        public void Then_Included_Schema_Names_Are_Correct()
        {
            var actualSchemaNames = _schemataUnderTest.Select(schema => schema.Name);

            Assert.That(actualSchemaNames, Is.EquivalentTo(_expectedSchemaNames).IgnoreCase);
        }
    }
}