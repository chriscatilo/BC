using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models.Enums;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture(SchemaKey.WithSubCategory, null, "default", IncidentCommand.Save, IncidentCommand.Raise)]
    // Given incident includes sub category field
    // When incident is new
    // Then schema named default exists
    // And augments exists for commands Save and Raise
    [TestFixture(SchemaKey.WithSubCategory, IncidentStatus.Draft, "default", IncidentCommand.Raise, IncidentCommand.AddCandidate)]
    // Given incident includes sub category field
    // When incident is Draft
    // Then schema named default exists
    // And augments exists for commands Raise, SaveCandidate
    [TestFixture(SchemaKey.WithSubCategory, IncidentStatus.Submitted, "default", IncidentCommand.AddCandidate)]
    // Given incident includes sub category field
    // When incident is Submitted
    // Then schema named default exists
    // And no augments exists
    [TestFixture(SchemaKey.WithSubCategory, IncidentStatus.InProgress, "default", IncidentCommand.Close, IncidentCommand.AddCandidate)]
    // Given incident includes sub category field
    // When incident is InProgress
    // Then schema named default exists
    // And augments exists for commands Close
    [TestFixture(SchemaKey.WithSubCategory, IncidentStatus.Closed, "default")]
    // Given incident includes sub category field
    // When incident is Closed
    // Then schema named default exists
    // And no augments exists
    public partial class TestSchemataNamesIncluded
    {
    }
}