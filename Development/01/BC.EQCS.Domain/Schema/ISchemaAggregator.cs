using System.Collections.Generic;

namespace BC.EQCS.Domain.Schema
{
    public interface ISchemaAggregator<TModelAttributes, TPersistModel, in TSchemaCriterion, in TEvent>
    {
        ISchemaAggregator<TModelAttributes, TPersistModel, TSchemaCriterion, TEvent> ForCriterion(TSchemaCriterion criterion);

        ISchemaAggregator<TModelAttributes, TPersistModel, TSchemaCriterion, TEvent> ForEvent(TEvent @event);

        IEnumerable<SchemaMemberAggregate<TModelAttributes, TPersistModel>> Aggregate(int modelId);

        IEnumerable<SchemaMemberAggregate<TModelAttributes, TPersistModel>> AggregateForNewModel();
    }
}