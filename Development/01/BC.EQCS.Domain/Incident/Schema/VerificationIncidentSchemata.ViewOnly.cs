using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class VerificationIncidentSchemata
    {
        private class WhenViewOnly : ModelSchema<IncidentAttributes>
        {
            private WhenViewOnly()
            {
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.IncidentTime, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.Description, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ImmediateActionTaken, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.Product, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.RaisedBy, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.TestLocation, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.Category, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.SubCategory, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.RiskRating, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ResidualRiskRating, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.TestDate, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.NoOfCandidates, constraint: ValueConstraint.ViewOnly);

                BuildFor(model => model.ReferringOrgSurname, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgFirstnames, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgJobTitle, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgEmail, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgType, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgCountry, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrganisation, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.ReferringOrgExists, constraint: ValueConstraint.ViewOnly);

                BuildFor(model => model.ReportUkvi, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.UkviFollowUpDate, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.UkviImmediateReportType, constraint: ValueConstraint.ViewOnly);

                BuildFor(model => model.CandidateNumber, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateSurname, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateFirstnames, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateAddress, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateDateOfBirth, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateGender, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateIdDocumentNumber, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateTrfNumber, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateDateTrfCancelled, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateUKVIRefNumber, constraint: ValueConstraint.ViewOnly);
                BuildFor(model => model.CandidateNationality, constraint: ValueConstraint.ViewOnly);
            }

            public static WhenViewOnly Create()
            {
                return new WhenViewOnly();
            }
        }
    }
}