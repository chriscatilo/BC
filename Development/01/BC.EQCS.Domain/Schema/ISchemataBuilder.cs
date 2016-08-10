using System.Collections.Generic;

namespace BC.EQCS.Domain.Schema
{
    public interface ISchemataBuilder<TModel, in TStatus, in TAugmentKey>
    {
        ISchemataBuilder<TModel, TStatus, TAugmentKey> ForStatus(TStatus status);
        ISchemataBuilder<TModel, TStatus, TAugmentKey> IncludeAugmentsFor(params TAugmentKey[] commands);
        IEnumerable<NamedSchema<TModel>> Build();
    }
}