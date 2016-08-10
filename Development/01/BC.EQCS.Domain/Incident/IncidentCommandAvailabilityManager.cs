using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Domain.Incident
{
    public class IncidentCommandAvailabilityManager : ICommandAvailabilityManager<IncidentCommand>
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly IContextResolver _context;
        private readonly IRepository<IncidentMasterModel> _modelRepository;
        private readonly IAspectRepository<IncidentActionModel, IncidentMasterModel> _modelActionRepository;
        private readonly ILookup<IncidentStatus, IncidentCommand> _statusCommandMapping;
        private ICollection<IncidentCommand> _commands;
        private IncidentMasterModel _incident;

        public IncidentCommandAvailabilityManager(
            IRepository<IncidentMasterModel> modelRepository,
            IAspectRepository<IncidentActionModel, IncidentMasterModel> modelActionRepository,
            ILookup<IncidentStatus, IncidentCommand> statusCommandMapping,
            IAssetAuthoriser authoriser,
            IContextResolver context)
        {
            _modelRepository = modelRepository;
            _modelActionRepository = modelActionRepository;
            _statusCommandMapping = statusCommandMapping;
            _authoriser = authoriser;
            _context = context;
        }

        public IEnumerable<IncidentCommand> GetByModelId(int id)
        {
            GetIncidentFromRepository(id);

            if (_authoriser.IsReadOnly(_incident.Category))
            {
                return new List<IncidentCommand>();
            }

            GetTheAvailableCommandsFromIncidentStatus();

            RemoveTheCommandsThatTheUserDoesNotHaveSecurityAccessFor();

            RemoveCloseWhereOpenIncidentActions();

            PreventSelectedUsersFromSaving();

            return _commands;
        }

        private void PreventSelectedUsersFromSaving()
        {
            var incidentHasBeenSubmitted = _incident.Status >= IncidentStatus.Submitted;

            if (incidentHasBeenSubmitted)
            {
                var userIsNotAllowedToSaveAfterTheIncidentHasBeenSubmitted =
                    !_authoriser.IsAuthorised(AssetType.IncidentPostSubmitSaveAllowed);

                if (userIsNotAllowedToSaveAfterTheIncidentHasBeenSubmitted)
                {
                    // Remove the save commands
                    _commands.Remove(IncidentCommand.Save);
                }

                if (_authoriser.IsIncidentClassViewOnly(_incident.IncidentClass))
                {
                    _commands.Remove(IncidentCommand.AddCandidate);
                }
            }
        }

        public IEnumerable<IncidentCommand> GetForNewModel()
        {
            GetCommandsForNewIncident();

            RemoveTheCommandsThatTheUserDoesNotHaveSecurityAccessFor();

            return _commands;
        }

        public bool IsAvailable(int id, IncidentCommand command)
        {
            _commands = GetByModelId(id).ToList();

            return _commands.Any(cmd => cmd == command);
        }

        private void GetTheAvailableCommandsFromIncidentStatus()
        {
            _commands = _statusCommandMapping[_incident.Status].ToList();
        }

        private void GetIncidentFromRepository(int id)
        {
            _incident = _modelRepository.GetById(id);

            if (_incident == null)
            {
                throw new ModelNotFoundException();
            }
        }

        private void GetCommandsForNewIncident()
        {
            _commands = _statusCommandMapping[IncidentStatus.None].ToList();
        }

        private void RemoveTheCommandsThatTheUserDoesNotHaveSecurityAccessFor()
        {
            _commands = (from incidentCommand in _commands
                let securityAssetKey = incidentCommand.ConvertToSecurityAssetKey()
                where _authoriser.IsAuthorised(securityAssetKey)
                select incidentCommand).ToList();
        }

        private void RemoveCloseWhereOpenIncidentActions()
        {
            var anyOpenActions = _modelActionRepository.GetFor(_incident).Any(action => action.Status != IncidentActionStatus.Closed);

            if (anyOpenActions)
            {
                _commands.Remove(IncidentCommand.Close);
            }
            
        }
    }
}