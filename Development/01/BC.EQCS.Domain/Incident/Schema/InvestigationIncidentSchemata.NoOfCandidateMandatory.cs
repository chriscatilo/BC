using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class InvestigationIncidentSchemata
    {
        private class NoOfCandidatesMandatory : ModelSchema<IncidentAttributes>
        {
            private NoOfCandidatesMandatory()
            {
                BuildFor(model => model.NoOfCandidates, constraint: ValueConstraint.Mandatory);
            }

            public static NoOfCandidatesMandatory Create()
            {
                return new NoOfCandidatesMandatory();
            }
        }
    }
}