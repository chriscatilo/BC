using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture(SchemaKey.Verification, null, "default", IncidentCommand.Save, IncidentCommand.Raise)]
    // Given incident is verification
    // When incident is new
    // Then schema named default exists
    // And augments exists for commands Save and Raise
    [TestFixture(SchemaKey.Verification, IncidentStatus.Draft, "default", IncidentCommand.Raise, IncidentCommand.AddCandidate)]
    // Given incident is verification
    // When incident is Draft
    // Then schema named default exists
    // And augments exists for commands Raise
    [TestFixture(SchemaKey.Verification, IncidentStatus.Submitted, "default", IncidentCommand.AddCandidate)]
    // Given incident is verification
    // When incident is Submitted
    // Then schema named default exists
    // And no augments exists
    [TestFixture(SchemaKey.Verification, IncidentStatus.InProgress, "default", IncidentCommand.Close, IncidentCommand.AddCandidate)]
    // Given incident is verification
    // When incident is InPrgress
    // Then schema named default exists
    // And augments exists for commands Close
    [TestFixture(SchemaKey.Verification, IncidentStatus.Closed, "default")]
    // Given incident is verification
    // When incident is Closed
    // Then schema named default exists
    // And no augments exists
    public partial class TestSchemataNamesIncluded
    {
    }
}