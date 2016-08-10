using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [Ignore]
    public abstract class IncidentSchemaBuildTest
    {
        private ModelSchema<IncidentAttributes> _fieldsUnderTest;
        protected abstract string Given_Schema_Key();

        protected virtual IncidentStatus? Given_Current_Incident_Status()
        {
            return null;
        }

        protected virtual IncidentCommand? Given_Incident_Command()
        {
            return null;
        }

        [TestFixtureSetUp]
        public void When_Incident_Model_Schema_Is_Created()
        {
            // given asset key
            var key = Given_Schema_Key();

            // given available commands
            var givenAvailableCommand = Given_Incident_Command();

            // given status
            var currentStatus = Given_Current_Incident_Status();

            // when schemata is built
            var availableTransitions = new IncidentAvailableTransitions();
            var builderDirector = new IncidentSchemaFactory(availableTransitions);
            var schemataBuilder = builderDirector.CreateBuilderByKey(key);
            if (currentStatus != null)
            {
                schemataBuilder = schemataBuilder.ForStatus((IncidentStatus) currentStatus);
            }
            if (givenAvailableCommand != null)
            {
                schemataBuilder = schemataBuilder.IncludeAugmentsFor((IncidentCommand) givenAvailableCommand);
            }
            var result = schemataBuilder.Build().ToList();

            Assert.That(result.Any(item => item.Name.Equals("default")), Is.True, "Default model schema does not exist");
            var defaultSchema = result.First(schema => schema.Name.Equals("default"));

            // And no available command exists
            if (givenAvailableCommand == null)
            {
                // then test the default schema
                _fieldsUnderTest = defaultSchema.Members;
            }

            // else no augment exists for available command
            else if (!result.Any(item => item.Name.EqualsCaseInsensitive(givenAvailableCommand.ToString())))
            {
                // then test the default schema
                _fieldsUnderTest = defaultSchema.Members;
            }

            // else if augment exists for available command
            else
            {
                var augmentSchema =
                    result.First(item => item.Name.EqualsCaseInsensitive(givenAvailableCommand.ToString()));

                // then test the default schema merged with the command-labeled augment
                _fieldsUnderTest = defaultSchema.Members.Merge(augmentSchema.Members);
            }
        }

        [Test]
        public void Then_Non_Applicable_Fields_Are_Correct()
        {
            var actualFields = GetActualFields(field => field.Constraint == ValueConstraint.NotApplicable);

            var expectedFields = EntityProperties<IncidentAttributes>.Create();

            Given_Expected_Non_Applicable_Fields(expectedFields);

            var expectedFieldNames = expectedFields.Select(prop => prop.Name).ToArray();

            Assert.That(actualFields, Is.EquivalentTo(expectedFieldNames).IgnoreCase,
                "Unexpected Non applicable fields : " + ExceptList(actualFields, expectedFieldNames));
        }

        private string ExceptList(IEnumerable<string> actualNames, IEnumerable<string> expectedNames)
        {
            var sb = new StringBuilder("In Actual: ");

            actualNames.Except(expectedNames).ToList().ForEach(name => sb.AppendFormat("{0},", name));

            sb.Append("; In Expected: ");

            expectedNames.Except(actualNames).ToList().ForEach(name => sb.AppendFormat("{0},", name));

            return sb.ToString();
        }

        protected abstract void Given_Expected_Non_Applicable_Fields(EntityProperties<IncidentAttributes> expectedFields);

        [Test]
        public void Then_Optional_Fields_Are_Correct()
        {
            var actualFields = GetActualFields(field => field.Constraint == ValueConstraint.Optional);

            var expectedFields = EntityProperties<IncidentAttributes>.Create();

            Given_Expected_Optional_Fields(expectedFields);

            var expectedFieldNames = expectedFields.Select(prop => prop.Name).ToArray();

            Assert.That(actualFields, Is.EquivalentTo(expectedFieldNames).IgnoreCase,
                "Unexpected Optional fields : " + ExceptList(actualFields, expectedFieldNames));
        }

        protected abstract void Given_Expected_Optional_Fields(EntityProperties<IncidentAttributes> expectedFields);

        [Test]
        public void Then_Mandatory_Fields_Are_Correct()
        {
            var actualFields = GetActualFields(field => field.Constraint == ValueConstraint.Mandatory);

            var expectedFields = EntityProperties<IncidentAttributes>.Create();

            Given_Expected_Mandatory_Fields(expectedFields);

            var expectedFieldNames = expectedFields.Select(prop => prop.Name).ToArray();

            Assert.That(actualFields, Is.EquivalentTo(expectedFieldNames).IgnoreCase,
                "Unexpected Mandatory fields : " + ExceptList(actualFields, expectedFieldNames));
        }

        protected abstract void Given_Expected_Mandatory_Fields(EntityProperties<IncidentAttributes> expectedFields);

        [Test]
        public void Then_View_Only_Fields_Are_Correct()
        {
            var actualFields = GetActualFields(field => field.Constraint == ValueConstraint.ViewOnly);

            var expectedFields = EntityProperties<IncidentAttributes>.Create();

            Given_Expected_View_Only_Fields(expectedFields);

            var expectedFieldNames = expectedFields.Select(prop => prop.Name).ToArray();

            Assert.That(actualFields, Is.EquivalentTo(expectedFieldNames).IgnoreCase,
                "Unexpected View Only fields : " + ExceptList(actualFields, expectedFieldNames));
        }

        [Test]
        public void Then_Restricted_Fields_Are_Correct()
        {
            var actualFields = GetActualFields(field => field.Constraint == ValueConstraint.Restricted);

            var expectedFields = EntityProperties<IncidentAttributes>.Create();

            Given_Expected_Restricted_Fields(expectedFields);

            var expectedNames = expectedFields.Select(prop => prop.Name).ToArray();

            Assert.That(actualFields, Is.EquivalentTo(expectedNames).IgnoreCase,
                "Unexpected Restricted fields : " + ExceptList(actualFields, expectedNames));
        }

        [Test]
        public void Then_All_Fields_Are_Tested()
        {
            var expectedFields = EntityProperties<IncidentAttributes>.Create();
            Given_Expected_Non_Applicable_Fields(expectedFields);
            Given_Expected_Optional_Fields(expectedFields);
            Given_Expected_Mandatory_Fields(expectedFields);
            Given_Expected_View_Only_Fields(expectedFields);
            Given_Expected_Restricted_Fields(expectedFields);
            Given_Expected_ServerResolved_Fields(expectedFields);

            var fieldsNotTested = TypeHelpers.GetPropertiesOf(typeof(IncidentAttributes)).Select(info => info.Name)
                .Except(expectedFields.Select(info => info.Name))
                .Except(new[] {"Candidates"});

            Assert.That(fieldsNotTested, Is.Empty, "There are properties that are not being tested");
        }

        protected abstract void Given_Expected_ServerResolved_Fields(EntityProperties<IncidentAttributes> expectedFields);

        protected abstract void Given_Expected_Restricted_Fields(EntityProperties<IncidentAttributes> expectedFields);
        
        protected abstract void Given_Expected_View_Only_Fields(EntityProperties<IncidentAttributes> expectedFields);

        private IEnumerable<string> GetActualFields(Func<MemberSchema, bool> predicate)
        {
            var actualFields = _fieldsUnderTest.Where(predicate).Select(field => field.ModelProperty.Name);
            return actualFields;
        }
    }
}