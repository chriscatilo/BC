using System.Collections.Generic;

namespace BC.EQCS.Contracts
{
    public interface ISchemaKeyRepository<in TSchemaCriterion>
    {
        string Get(IEnumerable<string> roles);
        
        string Get(IEnumerable<string> roles, TSchemaCriterion criterion);
    }
}