using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    public class IncidentCandidatePersistanceSchemaAggregator : IncidentSchemaAggregator<IncidentCandidateModel>
    {
        public IncidentCandidatePersistanceSchemaAggregator(
            ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> schemaBuildDirector,
            IIncidentAttributeMapping<IncidentCandidateModel> attributeMap)
            : base(schemaBuildDirector, attributeMap)
        {
        }
    }
}