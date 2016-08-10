using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class VerificationIncidentSchemata
    {
        private class WhenDraft : ModelSchema<IncidentAttributes>
        {
            private WhenDraft()
            {
                BuildFor(model => model.Category, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Description, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.Mandatory);
            }

            public static WhenDraft Create()
            {
                return new WhenDraft();
            }
        }
    }
}