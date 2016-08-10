using BC.EQCS.Contracts;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    /// <summary>
    /// * This builder aggregates the schemata of IncidentModel from a given schema key (e.g. Class = Verification) 
    /// and event (e.g. Save, Raise, etc) in order to get the value constraints of each incident attribute.
    /// * For each incident model attribute, the current and input object is passed to the aggregated strategy
    /// allowing it to update the current object.
    /// * The resulting object is returned to the client for it purposes (e.g. persist to database, send to UI client
    /// </summary>
    public class IncidentModelUpdater : IModelUpdater<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly IRepository<IncidentModel> _repository;

        private readonly
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
            _schemaAggregator;

        private readonly IModelUpdateStrategy<IncidentModel> _strategy;
        private IncidentCommand _command;
        private IncidentSchemaKeyCriterion _criterion;

        public IncidentModelUpdater(IRepository<IncidentModel> repository,
            ISchemaAggregator<IncidentAttributes, IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand>
                schemaAggregator,
            IModelUpdateStrategy<IncidentModel> strategy)
        {
            _repository = repository;
            _schemaAggregator = schemaAggregator;
            _strategy = strategy;
        }

        public IModelUpdater<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> ForCriterion(
            IncidentSchemaKeyCriterion criterion)
        {
            _criterion = criterion;
            return this;
        }

        public IModelUpdater<IncidentModel, IncidentSchemaKeyCriterion, IncidentCommand> ForEvent(IncidentCommand @event)
        {
            _command = @event;
            return this;
        }

        public IncidentModel Update(IncidentModel update)
        {
            return Update(0, update);
        }

        public IncidentModel Update(int modelId, IncidentModel update)
        {
            var source = _repository.GetById(modelId) ?? new IncidentModel();

            var schemaAggregatorForEvent = _schemaAggregator
                .ForCriterion(_criterion)
                .ForEvent(_command);

            var aggregate = modelId == 0
                ? schemaAggregatorForEvent.AggregateForNewModel()
                : schemaAggregatorForEvent.Aggregate(modelId);


            foreach (var item in aggregate)
            {
                _strategy.Execute(item.TargetMember, item.SchemaMember.Constraint, source, update);
            }

            return source;
        }
    }
}