using System.Collections.Generic;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Domain.Incident.Schema
{
    public class IncidentSchemaFactory : ISchemaBuilderFactory<IncidentAttributes, IncidentStatus, IncidentCommand>
    {
        private static readonly IDictionary<string, IModelSchemata<IncidentAttributes>> Schemata =
            new Dictionary<string, IModelSchemata<IncidentAttributes>>
            {
                {SchemaKey.Standard, new StandardIncidentSchemata()},
                {SchemaKey.StandardViewOnly, new StandardIncidentViewOnlySchemata()},
                {SchemaKey.WithSubCategory, new WithSubCategoryIncidentSchemata()},
                {SchemaKey.Investigation, new InvestigationIncidentSchemata()},
                {SchemaKey.Verification, new VerificationIncidentSchemata()},
                {SchemaKey.TestCentre, new TestCentreIncidentSchemata()},
                {SchemaKey.TestCentreViewOnly, new TestCenterIncidentViewOnlySchemata()}
            };

        private readonly ICommandTransitionMaps<IncidentCommand, IncidentStatus> _availableTransitions;

        public IncidentSchemaFactory(ICommandTransitionMaps<IncidentCommand, IncidentStatus> availableTransitions)
        {
            _availableTransitions = availableTransitions;
        }

        public ISchemataBuilder<IncidentAttributes, IncidentStatus, IncidentCommand> CreateBuilderByKey(string key)
        {
            var builder = !Schemata.ContainsKey(key)
                ? null
                : new IncidentSchemataBuilder(Schemata[key], _availableTransitions);

            return builder;
        }
    }
}