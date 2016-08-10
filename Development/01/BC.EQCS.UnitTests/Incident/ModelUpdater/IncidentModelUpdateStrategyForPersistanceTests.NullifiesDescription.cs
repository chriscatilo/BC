using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident.ModelUpdater;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.ModelUpdater
{
    public partial class IncidentModelUpdateStrategyForPersistanceTests
    {
        [TestFixture]
        // Given existing incident with description
        // And description is Not Applicable
        // When incident is updated
        // Then description is null
        public class NullifiesDescription : IncidentModelUpdaterTest
        {
            protected override ValueConstraint Given_Value_Contraint()
            {
                return ValueConstraint.NotApplicable;
            }

            protected override string Given_Original_Incident_Description()
            {
                return "Orginal";
            }

            protected override IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey> Given_Update_Strategy()
            {
                return new IncidentModelUpdateStrategyForPersistance();
            }

            protected override void AssertTest(IncidentModel actualResult)
            {
                Assert.That(actualResult.Description, Is.Null);
            }
        }
    }
}