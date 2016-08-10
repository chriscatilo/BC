using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Models.Api;

namespace BC.EQCS.Web.Utils
{
    public static class CommandAvailabilityExtensions
    {
        // this is a lookup list of url route names mapped to command (e.g. Save, Raise) and scenario (new or existing incident)
        private static readonly IList<CommandRouteMap> CommandRouteMapping = new[]
        {
            // New incident mapping
            new CommandRouteMap
            {
                ForScenario = ForScenario.NewIncident,
                Command = IncidentCommand.Save,
                RouteName = ApiRoutes.Incident.Name,
                AllowsPersistence = true
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.NewIncident,
                Command = IncidentCommand.Raise,
                RouteName = ApiRoutes.IncidentSubmission.Name,
                AllowsPersistence = true
            },

            // Existing incident mapping
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Delete,
                RouteName = ApiRoutes.IncidentById.Name
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Save,
                RouteName = ApiRoutes.IncidentById.Name,
                AllowsPersistence = true
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Raise,
                RouteName = ApiRoutes.IncidentByIdSubmission.Name,
                AllowsPersistence = true
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Accept,
                RouteName = ApiRoutes.IncidentByIdAcceptance.Name,
                AllowsPersistence = true
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Reject,
                RouteName = ApiRoutes.IncidentByIdRejection.Name
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.Close,
                RouteName = ApiRoutes.IncidentByIdClosure.Name,
                AllowsPersistence = true
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.ReOpen,
                RouteName = ApiRoutes.IncidentByIdReopening.Name
            },
            new CommandRouteMap
            {
                ForScenario = ForScenario.ExitingIncident,
                Command = IncidentCommand.AddCandidate,
                RouteName = ApiRoutes.IncidentByIdCandidate.Name
            },
        };

        /// <summary>
        /// Create a list of Url links to available commands for a given incident
        /// </summary>
        public static IEnumerable<AvailableCommandLink> CreateAvailableCommandLinks(
            this IEnumerable<IncidentCommand> commands, UrlHelper urlHelper, int id)
        {
            var commandRoutesForExistingIncident =
                CommandRouteMapping.Where(map => map.ForScenario == ForScenario.ExitingIncident);

            var routes = commands
                .Join(commandRoutesForExistingIncident, cmd => cmd, map => map.Command, (cmd, map) => map)
                .Select(map =>
                    new AvailableCommandLink
                    {
                        Name = map.Command,
                        Href = urlHelper.GetHrefFromRouteName(map.RouteName, new { id }),
                        AllowsPersistence = map.AllowsPersistence
                    });

            return routes;
        }

        /// <summary>
        /// Create a list of Url links to available commands for a new incident
        /// </summary>
        public static IEnumerable<AvailableCommandLink> CreateAvailableCommandLinks(
            this IEnumerable<IncidentCommand> commands, UrlHelper urlHelper)
        {
            var commandRoutesForNewIncident =
                CommandRouteMapping.Where(map => map.ForScenario == ForScenario.NewIncident);

            var routes = commands
                .Join(commandRoutesForNewIncident, cmd => cmd, map => map.Command, (cmd, map) => map)
                .Select(map =>
                    new AvailableCommandLink
                    {
                        Name = map.Command,
                        Href = urlHelper.GetHrefFromRouteName(map.RouteName),
                        AllowsPersistence = map.AllowsPersistence
                    });

            return routes;
        }

        [Flags]
        private enum ForScenario
        {
            NewIncident = 1,
            ExitingIncident = 2
        }

        private class CommandRouteMap
        {
            public ForScenario ForScenario { get; set; }
            public IncidentCommand Command { get; set; }
            public string RouteName { get; set; }
            public bool AllowsPersistence { get; set; }
        }
    }
}