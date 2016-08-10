using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class StandardIncidentSchemata
    {
        internal class WhenSavingCandidate : ModelSchema<IncidentAttributes>
        {
            private WhenSavingCandidate()
            {
                BuildFor(model => model.CandidateNumber, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateSurname, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateFirstnames, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateDateOfBirth, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateGender, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateIdDocumentNumber, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.CandidateNationality, constraint: ValueConstraint.Mandatory);

                BuildFor(model => model.CandidateAddress, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateTrfNumber, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateDateTrfCancelled, constraint: ValueConstraint.Optional);
                BuildFor(model => model.CandidateUKVIRefNumber, constraint: ValueConstraint.Optional);
            }

            public static WhenSavingCandidate Create()
            {
                return new WhenSavingCandidate();
            }
        }
    }
}