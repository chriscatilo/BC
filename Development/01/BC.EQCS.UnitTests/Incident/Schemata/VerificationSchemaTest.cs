using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Schemata
{
    [TestFixture]
    // Given incident is new
    // And incident is Verfication
    public class VerificationSchemaTest : IncidentSchemaBuildTest
    {
        protected override string Given_Schema_Key()
        {
            return SchemaKey.Verification;
        }

        protected override void Given_Expected_Non_Applicable_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.ResidualRiskRating);
        }

        protected override void Given_Expected_Optional_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
            expectedFields
                .Add(model => model.IncidentDate)
                .Add(model => model.IncidentTime)
                .Add(model => model.Description)
                .Add(model => model.ImmediateActionTaken)
                .Add(model => model.Product)
                .Add(model => model.RaisedBy)
                .Add(model => model.TestCentre)
                .Add(model => model.TestLocation)
                .Add(model => model.Category)
                .Add(model => model.RiskRating)
                .Add(model => model.SubCategory)
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
                .Add(model => model.CandidateNationality)

                .Add(model => model.ReportUkvi)
                .Add(model => model.UkviFollowUpDate)
                .Add(model => model.UkviImmediateReportType);
        }

        protected override void Given_Expected_Mandatory_Fields(EntityProperties<IncidentAttributes> expectedFields)
        {
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
                .Add(model => model.IELTSRegion)
                .Add(model => model.FormalId)
                .Add(model => model.LoggedByUser)
                .Add(model => model.Status)
                .Add(model => model.CreateDate);
        }
    }
}