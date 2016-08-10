using System.Collections.Generic;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Validation
{
    public interface IIncidentAttributeMapping<TTargetModel> : IEnumerable<IModelAttributeMap<IncidentAttributes, TTargetModel>>
    {
    }
}