using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture(SchemaKey.TestCentre, null, "default", IncidentCommand.Save, IncidentCommand.Raise)]
    // Given incident is TestCentre
    // When incident is new
    // Then schema named default exists
    // And augments exists for commands Save and Raise
    [TestFixture(SchemaKey.TestCentre, IncidentStatus.Draft, "default", IncidentCommand.Raise, IncidentCommand.AddCandidate)]
    // Given incident is TestCentre
    // When incident is Draft
    // Then schema named default exists
    // And augments exists for commands Raise
    [TestFixture(SchemaKey.TestCentre, IncidentStatus.Submitted, "default", IncidentCommand.AddCandidate)]
    // Given incident is TestCentre
    // When incident is Submitted
    // Then schema named default exists
    // And no augments exists
    [TestFixture(SchemaKey.TestCentre, IncidentStatus.InProgress, "default", IncidentCommand.AddCandidate)]
    // Given incident is TestCentre
    // When incident is InProgress
    // Then schema named default exists
    // And no augments exists
    [TestFixture(SchemaKey.TestCentre, IncidentStatus.Closed, "default")]
    // Given incident is TestCentre
    // When incident is Closed
    // Then schema named default exists
    // And no augments exists
    public partial class TestSchemataNamesIncluded
    {
    }
}