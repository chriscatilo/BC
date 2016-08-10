using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class WithSubCategoryIncidentSchemata
    {
        internal class WhenOpen : ModelSchema<IncidentAttributes>
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

                BuildFor(model => model.ReportUkvi, constraint: ValueConstraint.Mandatory);
            }

            public static WhenOpen Create()
            {
                return new WhenOpen();
            }
        }
    }
}