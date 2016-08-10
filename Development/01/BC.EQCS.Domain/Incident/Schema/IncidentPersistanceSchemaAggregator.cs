using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    public class IncidentPersistanceSchemaAggregator : IncidentSchemaAggregator<IncidentModel>
    {
        public IncidentPersistanceSchemaAggregator(
            ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> schemaBuildDirector,
            IIncidentAttributeMapping<IncidentModel> attributeMap)
            : base(schemaBuildDirector, attributeMap)
        {
        }
    }
}