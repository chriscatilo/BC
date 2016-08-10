using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class TestCentreIncidentSchemata
    {
        private class WhenRaising : ModelSchema<IncidentAttributes>
        {
            private WhenRaising()
            {
                BuildFor(model => model.IncidentDate, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.IncidentTime, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Description, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Product, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.RaisedBy, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestCentre, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestLocation, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.Category, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.TestDate, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.ImmediateActionTaken, constraint: ValueConstraint.Mandatory);
                BuildFor(model => model.RiskRating, constraint: ValueConstraint.ServerResolved);
            }

            public static WhenRaising Create()
            {
                return new WhenRaising();
            }
        }
    }
}