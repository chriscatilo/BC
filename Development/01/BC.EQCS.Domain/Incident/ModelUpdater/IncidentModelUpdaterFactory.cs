using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    /// <summary>
    /// Creates an object update builder for Incident Model.
    /// * The update strategies are fetched by their keys (e.g. For Persistence, ForNullifySubCategory)
    /// and the strategies are aggregated into one using decoration.
    /// * The aggregated strategy is then passed to the update builder (see notes on IncidentModelUpdater)
    /// </summary>
    public class IncidentModelUpdaterFactory :
        IModelUpdaterFactory<
            IncidentModel,
            IncidentSchemaKeyCriterion,
            IncidentCommand,
            IncidentModelUpdateStrategyKey>
    {
        private readonly IRepository<IncidentModel> _repository;

        private readonly
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
            _schemaAggregator;

        private readonly IDictionary<
            IncidentModelUpdateStrategyKey,
            IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey>> _updateStrategies;

        public IncidentModelUpdaterFactory(
            IRepository<IncidentModel> repository,
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator,
            IEnumerable<IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey>> updateStrategies)
        {
            _repository = repository;
            _schemaAggregator = schemaAggregator;
            _updateStrategies = updateStrategies.ToDictionary(strategy => strategy.Key);
        }

        public IModelUpdater<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> CreateUpdater(
            params IncidentModelUpdateStrategyKey[] strategies)
        {
            // aggregate model strategies into one thru decoration
            var updateStrategy = _updateStrategies
                .Join(strategies, pair => pair.Key, key => key, (pair, key) => pair.Value)
                .Aggregate((aggregate, nextStrategy) => nextStrategy.Decorate(aggregate));

            var updater = new IncidentModelUpdater(_repository, _schemaAggregator, updateStrategy);

            return updater;
        }
    }
}