using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident.ModelUpdater;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.ModelUpdater
{
    public partial class IncidentModelUpdateStrategyForPresentationTests
    {
        [TestFixture(ValueConstraint.NotApplicable)]
        [TestFixture(ValueConstraint.Restricted)]
        public class NullifiesDescription : IncidentModelUpdaterTest
        {
            private readonly ValueConstraint _constraint;

            public NullifiesDescription(ValueConstraint constraint)
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

            protected override IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey> Given_Update_Strategy()
            {
                return new IncidentModelUpdateStrategyForPresentation();
            }

            protected override void AssertTest(IncidentModel actualResult)
            {
                Assert.That(actualResult.Description, Is.Null);
            }
        }
    }
}