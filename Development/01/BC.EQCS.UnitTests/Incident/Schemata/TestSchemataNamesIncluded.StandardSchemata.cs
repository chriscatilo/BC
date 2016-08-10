using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture(SchemaKey.Standard, null, "default", IncidentCommand.Save, IncidentCommand.Raise)]
    // Given incident is in standard form
    // When incident is new
    // Then schema named default exists
    // And augments exists for commands Save and Raise
    [TestFixture(SchemaKey.Standard, IncidentStatus.Draft, "default", IncidentCommand.Raise, IncidentCommand.AddCandidate)]
    // Given incident is in standard form
    // When incident is Draft
    // Then schema named default exists
    // And augments exists for commands Raise, SaveCandidate
    [TestFixture(SchemaKey.Standard, IncidentStatus.Submitted, "default", IncidentCommand.AddCandidate)]
    // Given incident is in standard form
    // When incident is Submitted
    // Then schema named default exists
    // And no augments exists
    [TestFixture(SchemaKey.Standard, IncidentStatus.InProgress, "default", IncidentCommand.Close, IncidentCommand.AddCandidate)]
    // Given incident is in standard form
    // When incident is InProgress
    // Then schema named default exists
    // And augments exists for commands Close
    [TestFixture(SchemaKey.Standard, IncidentStatus.Closed, "default")]
    // Given incident is in standard form
    // When incident is Draft
    // Then schema named default exists
    // And no augments exists
    public partial class TestSchemataNamesIncluded
    {
    }
}