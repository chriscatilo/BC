using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident.ModelUpdater;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.ModelUpdater
{
    public partial class IncidentModelUpdateStrategyForPersistanceTests
    {
        [TestFixture(ValueConstraint.Optional)]
        [TestFixture(ValueConstraint.Mandatory)]
        // Given existing incident with description
        // And description is <Constraint>
        // And description has changed
        // When incident is updated
        // Then description has changed
        public class UpdatesDescription : IncidentModelUpdaterTest
        {
            private readonly ValueConstraint _constraint;

            public UpdatesDescription(ValueConstraint constraint)
            {
                _constraint = constraint;
            }

            protected override ValueConstraint Given_Value_Contraint()
            {
                return _constraint;
            }

            protected override string Given_Original_Incident_Description()
            {
                return "Orginal";
            }

            protected override string Given_Updated_Incident_Description()
            {
                return "Updated";
            }

            protected override IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey> Given_Update_Strategy()
            {
                return new IncidentModelUpdateStrategyForPersistance();
            }

            protected override void AssertTest(IncidentModel actualResult)
            {
                Assert.That(actualResult.Description, Is.EqualTo(Given_Updated_Incident_Description()));
            }
        }
    }
}