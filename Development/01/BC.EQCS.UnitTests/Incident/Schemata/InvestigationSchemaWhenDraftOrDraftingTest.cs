using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture(null, IncidentCommand.Save)]
    // Given incident is new
    // And incident is STANDARD form with sub category field
    // And incident is being saved
    [TestFixture(IncidentStatus.Draft, null)]
    // Given incident is DRAFT
    // And incident is STANDARD form with sub category field
    [TestFixture(IncidentStatus.Draft, IncidentCommand.Save)]
    // Given incident is Draft
    // And incident is STANDARD form with sub category field
    // And incident is being saved
    public class InvestigationSchemaWhenDraftOrDraftingTest : IncidentSchemaBuildTest
    {
        private readonly IncidentCommand? _command;
        private readonly IncidentStatus? _currentStatus;

        public InvestigationSchemaWhenDraftOrDraftingTest(IncidentStatus? currentStatus, IncidentCommand? command)
        {
            _currentStatus = currentStatus;
            _command = command;
        }

        protected override string Given_Schema_Key()
        {
            return SchemaKey.Investigation;
        }

        protected override IncidentCommand? Given_Incident_Command()
        {
            return _command;
        }

        protected override IncidentStatus? Given_Current_Incident_Status()
        {
            return _currentStatus;
        }

        protected override void Given_Expected_Non_Applicable_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.ResidualRiskRating);
        }

        protected override void Given_Expected_Optional_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.ImmediateActionTaken)
                .Add(model => model.Product)
                .Add(model => model.IncidentTime)
                .Add(model => model.RaisedBy)
                .Add(model => model.TestLocation)
                .Add(model => model.SubCategory)
                .Add(model => model.RiskRating)
                .Add(model => model.TestDate)
                .Add(model => model.NoOfCandidates)
                .Add(model => model.ReferringOrgSurname)
                .Add(model => model.ReferringOrgFirstnames)
                .Add(model => model.ReferringOrgJobTitle)
                .Add(model => model.ReferringOrgEmail)
                .Add(model => model.ReferringOrgType)
                .Add(model => model.ReferringOrgCountry)
                .Add(model => model.ReferringOrganisation)
                .Add(model => model.ReferringOrgExists)

                .Add(model => model.ReportUkvi)
                .Add(model => model.UkviFollowUpDate)
                .Add(model => model.UkviImmediateReportType)

                .Add(model => model.CandidateNumber)
                .Add(model => model.CandidateSurname)
                .Add(model => model.CandidateFirstnames)
                .Add(model => model.CandidateAddress)
                .Add(model => model.CandidateDateOfBirth)
                .Add(model => model.CandidateGender)
                .Add(model => model.CandidateIdDocumentNumber)
                .Add(model => model.CandidateTrfNumber)
                .Add(model => model.CandidateDateTrfCancelled)
                .Add(model => model.CandidateUKVIRefNumber)
                .Add(model => model.CandidateNationality);
        }

        protected override void Given_Expected_Mandatory_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.Category)
                .Add(model => model.Description)
                .Add(model => model.TestCentre)
                .Add(model => model.IncidentDate);
        }

        protected override void Given_Expected_ServerResolved_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.RowVersion)
                .Add(model => model.CandidateRowVersion);
        }

        protected override void Given_Expected_Restricted_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
        }

        protected override void Given_Expected_View_Only_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.Id)
                .Add(model => model.FormalId)
                .Add(model => model.IELTSRegion)
                .Add(model => model.LoggedByUser)
                .Add(model => model.Status)
                .Add(model => model.CreateDate);
        }
    }
}