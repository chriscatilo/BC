using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class VerificationIncidentSchemata
    {
        private class WhenOpen : ModelSchema<IncidentAttributes>
        {
            private WhenOpen()
            {
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.IncidentTime, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Description, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Product, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.RaisedBy, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestLocation, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Category, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.RiskRating, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestDate, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.NoOfCandidates, constraint: ValueConstraint.Mandatory);

                BuildFor(model => model.ReferringOrgSurname, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgFirstnames, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgJobTitle, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgEmail, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgType, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgCountry, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrganisation, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ReferringOrgExists, constraint: ValueConstraint.Mandatory);

                BuildFor(model => model.ReportUkvi, constraint: ValueConstraint.Mandatory);

                BuildFor(model => model.ResidualRiskRating, constraint: ValueConstraint.Restricted);
            }

            public static WhenOpen Create()
            {
                return new WhenOpen();
            }
        }
    }
}