using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class VerificationIncidentSchemata
    {
        private class Default : ModelSchema<IncidentAttributes>
        {
            public static Default Create()
            {
                return new Default();
            }

            private Default()
            {
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.Optional);
                BuildFor(model => model.IncidentTime, constraint: ValueConstraint.Optional);
                BuildFor(model => model.Description, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ImmediateActionTaken, constraint: ValueConstraint.Optional);
                BuildFor(model => model.Product, constraint: ValueConstraint.Optional);
                BuildFor(model => model.RaisedBy, constraint: ValueConstraint.Optional);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.Optional);
                BuildFor(model => model.TestLocation, constraint: ValueConstraint.Optional);
                BuildFor(model => model.Category, constraint: ValueConstraint.Optional);
                BuildFor(model => model.SubCategory, constraint: ValueConstraint.Optional);
                BuildFor(model => model.RiskRating, constraint: ValueConstraint.Optional);
                BuildFor(model => model.TestDate, constraint: ValueConstraint.Optional);
                BuildFor(model => model.NoOfCandidates, constraint: ValueConstraint.Optional);

                BuildFor(model => model.ReferringOrgSurname, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgFirstnames, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgJobTitle, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgEmail, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgType, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgCountry, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrganisation, constraint: ValueConstraint.Optional);
                BuildFor(model => model.ReferringOrgExists, constraint: ValueConstraint.Optional);

                BuildFor(model => model.ReportUkvi, constraint: ValueConstraint.Optional);
                BuildFor(model => model.UkviFollowUpDate, constraint: ValueConstraint.Optional);
                BuildFor(model => model.UkviImmediateReportType, constraint: ValueConstraint.Optional);

                BuildFor(model => model.CandidateNumber, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateSurname, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateFirstnames, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateAddress, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateDateOfBirth, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateGender, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateIdDocumentNumber, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateTrfNumber, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateDateTrfCancelled, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateUKVIRefNumber, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateNationality, constraint: ValueConstraint.Optional);
            }
        }
    }
}