using System;
using System.Collections.Generic;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal partial class StandardIncidentViewOnlySchemata : IModelSchemata<IncidentAttributes>
    {
        private static readonly IDictionary<string, ModelSchema<IncidentAttributes>> Schemata =
            new Dictionary<string, ModelSchema<IncidentAttributes>>(StringComparer.OrdinalIgnoreCase);

        static StandardIncidentViewOnlySchemata()
        {
            var defaultSchema =
                BaseIncidentSchema.Create()
                    .Merge(StandardIncidentSchemata.WhenViewOnly.Create());

            Schemata.Add("default", defaultSchema);
        }

        public ModelSchema<IncidentAttributes> Get(string key)
        {
            ModelSchema<IncidentAttributes> schema;
            if (!Schemata.TryGetValue(key, out schema))
            {
                // if key is non-existent then return empty schema
                schema = new ModelSchema<IncidentAttributes>();
            }
            return schema;
        }

        public ModelSchema<IncidentAttributes> GetDefault()
        {
            return Schemata["default"];
        }
    }
}