using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class TestCentreIncidentSchemata
    {
        private class WhenDraft : ModelSchema<IncidentAttributes>
        {
            private WhenDraft()
            {
                BuildFor(model => model.Category, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Description, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.Mandatory);

                BuildFor(model => model.ReferringOrgSurname, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgFirstnames, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgJobTitle, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgEmail, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgType, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgCountry, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrganisation, constraint: ValueConstraint.Restricted);
                BuildFor(model => model.ReferringOrgExists, constraint: ValueConstraint.Restricted);
            }

            public static WhenDraft Create()
            {
                return new WhenDraft();
            }
        }
    }
}