using System.Collections.Generic;

namespace BC.EQCS.Domain.Schema
{
    public interface ISchemaBuildDirector<TModel, in TKeyCriterion>
    {
        IEnumerable<NamedSchema<TModel>> GetSchemataForNewModel(TKeyCriterion criterion);
        IEnumerable<NamedSchema<TModel>> GetSchemata(int modelId, TKeyCriterion criterion);
    }
}