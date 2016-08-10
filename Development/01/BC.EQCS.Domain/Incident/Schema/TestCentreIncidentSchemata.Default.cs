using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class TestCentreIncidentSchemata
    {
        private class Default : ModelSchema<IncidentAttributes>
        {
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
                BuildFor(model => model.TestDate, constraint: ValueConstraint.Optional);
                BuildFor(model => model.NoOfCandidates, constraint: ValueConstraint.Optional);

                BuildFor(model => model.RiskRating, constraint: ValueConstraint.ServerResolved);
                BuildFor(model => model.SubCategory, constraint: ValueConstraint.ServerResolved);

                BuildFor(model => model.ReferringOrgSurname, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgFirstnames, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgJobTitle, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgEmail, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgType, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgCountry, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrganisation, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgExists, constraint: ValueConstraint.Restricted);

                BuildFor(model => model.ReportUkvi, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.UkviFollowUpDate, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.UkviImmediateReportType, constraint: ValueConstraint.Restricted);

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

            public static Default Create()
            {
                return new Default();
            }
        }
    }
}