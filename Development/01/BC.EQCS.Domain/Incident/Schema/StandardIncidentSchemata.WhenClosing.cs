using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class StandardIncidentSchemata
    {
        private class WhenClosing : ModelSchema<IncidentAttributes>
        {
            private WhenClosing()
            {
                BuildFor(model => model.ResidualRiskRating, constraint: ValueConstraint.Mandatory);
            }

            public static WhenClosing Create()
            {
                return new WhenClosing();
            }
        }
    }
}