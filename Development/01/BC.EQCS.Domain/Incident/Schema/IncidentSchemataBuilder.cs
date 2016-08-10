using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Incident.Schema
{
    public class IncidentSchemataBuilder : ISchemataBuilder<IncidentAttributes, IncidentStatus, IncidentCommand>
    {
        private readonly IModelSchemata<IncidentAttributes> _modelSchemata;
        private readonly ICommandTransitionMaps<IncidentCommand, IncidentStatus> _availableTransitions;
        private IEnumerable<IncidentCommand> _commands;
        private IncidentStatus? _status;
        
        internal IncidentSchemataBuilder(
            IModelSchemata<IncidentAttributes> modelSchemata, 
            ICommandTransitionMaps<IncidentCommand, IncidentStatus> availableTransitions)
        {
            _modelSchemata = modelSchemata;
            _availableTransitions = availableTransitions;
        }

        public ISchemataBuilder<IncidentAttributes, IncidentStatus, IncidentCommand> ForStatus(IncidentStatus status)
        {
            _status = status;
            return this;
        }

        public ISchemataBuilder<IncidentAttributes, IncidentStatus, IncidentCommand> IncludeAugmentsFor(params IncidentCommand[] commands)
        {
            _commands = commands;
            return this;
        }

        public IEnumerable<NamedSchema<IncidentAttributes>> Build()
        {
            var schemata = _modelSchemata;

            var defaultSchema = _status == null
                ? schemata.GetDefault()
                : schemata.GetDefault().Merge(schemata.Get(_status.ToString()));

            var result = new List<NamedSchema<IncidentAttributes>>
            {
                new NamedSchema<IncidentAttributes>
                {
                    Name = "default",
                    Members = defaultSchema
                }
            };

            var commands = _commands ?? new IncidentCommand[0];

            // get transitions from the given status and available commands
            // and create schema augments labeled with the available command
            var namedAugments = _availableTransitions
                .Where(tr => tr.PreStatus == (_status ?? IncidentStatus.None))
                .Join(commands, tr => tr.Command, cmd => cmd, (tr, cmd) => tr)
                .Select(transition => new NamedSchema<IncidentAttributes>
                {
                    Name = transition.Command.ToString().ToCamelCase(),
                    Members = schemata.Get(transition.ToString())
                })
                .Where(schema => schema.Members.Any());

            result.AddRange(namedAugments);

            return result;
        }
    }
}