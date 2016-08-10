using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;

namespace BC.EQCS.Repositories
{
    public class SchemaKeyRepository : ISchemaKeyRepository<IncidentSchemaKeyCriterion>
    {
        private readonly IEntityFactory _entityFactory;

        public SchemaKeyRepository(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public string Get(IEnumerable<string> roles)
        {
            var criterion = new IncidentSchemaKeyCriterion { IncidentClass = "ROOT" };

            return Get(roles, criterion);
        }

        public string Get(IEnumerable<string> roles, IncidentSchemaKeyCriterion keyCriterion)
        {
            var incidentClass = keyCriterion == null || string.IsNullOrEmpty(keyCriterion.IncidentClass)
                ? "ROOT"
                : keyCriterion.IncidentClass;

            var entities = _entityFactory.Create();

            var value = entities.ApplicationRoles
                .Join(roles, role => role.Code, code => code, (role, code) => role)
                .SelectMany(ar => ar.SchemaKeys)
                .Where(
                    key =>
                        key.IncidentClass.Code.Equals(incidentClass, StringComparison.InvariantCultureIgnoreCase))
                .Select(key => key.SchemaKey)
                .FirstOrDefault();

            if (value == null)
            {
                throw new Exception(GetSchemaKeyNullErrorMessage(roles, incidentClass));
            }

            return value;
        }

        private static string GetSchemaKeyNullErrorMessage(IEnumerable<string> roles, string incidentClass)
        {
            var allRolesText = new StringBuilder();
            var i = 0;
            foreach (var role in roles)
            {
                i++;
                allRolesText.Append(role);
                if (i < roles.Count() && roles.Count() > 1 )
                {
                    allRolesText.Append(", ");
                }
                

            }

            var message = "No SchemaKey found in any of the users roles (" + allRolesText + ") for the incidentClass (" +
                          incidentClass + ") please add a record to SchemaKeyMap";
            return message;
        }
    }
}