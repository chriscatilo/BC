using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.Schema
{
    public abstract class IncidentSchemaAggregator<TTargetModel> :
        ISchemaAggregator<IncidentAttributes, TTargetModel, IncidentSchemaKeyCriterion, IncidentCommand>
    {
        private readonly ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> _schemaBuildDirector;
        private readonly IIncidentAttributeMapping<TTargetModel> _attributeMap;
        private IncidentSchemaKeyCriterion _keyCriterion;
        private IncidentCommand? _event;

        protected IncidentSchemaAggregator(
            ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> schemaBuildDirector,
            IIncidentAttributeMapping<TTargetModel> attributeMap)
        {
            _schemaBuildDirector = schemaBuildDirector;
            _attributeMap = attributeMap;
        }

        public IEnumerable<SchemaMemberAggregate<IncidentAttributes, TTargetModel>> Aggregate(int modelId)
        {
            var namedSchemata = _schemaBuildDirector.GetSchemata(modelId, _keyCriterion).ToList();

            return GetAggregate(namedSchemata);
        }

        public IEnumerable<SchemaMemberAggregate<IncidentAttributes, TTargetModel>> AggregateForNewModel()
        {
            var namedSchemata = _schemaBuildDirector.GetSchemataForNewModel(_keyCriterion).ToList();

            return GetAggregate(namedSchemata);
        }

        private IEnumerable<SchemaMemberAggregate<IncidentAttributes, TTargetModel>> GetAggregate(List<NamedSchema<IncidentAttributes>> namedSchemata)
        {
            var defaultSchema = namedSchemata.First(schema => schema.Name == "default");

            var augmentSchema = _event == null
                ? null
                : namedSchemata.FirstOrDefault(
                    schema => schema.Name.Equals(_event.ToString(), StringComparison.InvariantCultureIgnoreCase));

            var mergedSchema = augmentSchema != null
                ? defaultSchema.Members.Merge(augmentSchema.Members)
                : defaultSchema.Members;

            // join the schema with the attributes model mapping
            var values = _attributeMap.Join(
                mergedSchema,
                map => TypeHelpers.GetPropertyByExpression(map.Attribute),
                schemaMember => schemaMember.ModelProperty,
                (map, schemaMember) => new SchemaMemberAggregate<IncidentAttributes, TTargetModel>
                {
                    AttributesMember = map.Attribute,
                    TargetMember = map.Target,
                    SchemaMember = schemaMember
                });

            return values;
        }

        public ISchemaAggregator<IncidentAttributes, TTargetModel, IncidentSchemaKeyCriterion, IncidentCommand>
            ForCriterion(IncidentSchemaKeyCriterion criterion)
        {
            _keyCriterion = criterion;
            return this;
        }

        public ISchemaAggregator<IncidentAttributes, TTargetModel, IncidentSchemaKeyCriterion, IncidentCommand> ForEvent
            (IncidentCommand @event)
        {
            _event = @event;
            return this;
        }
    }
}