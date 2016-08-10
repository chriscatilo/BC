using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Models.Api;
using BC.EQCS.Web.Utils;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentResourceController : ApiController
    {
        private readonly ICommandAvailabilityManager<IncidentCommand> _commandAvailabilityManager;
        private readonly IRepository<IncidentMasterModel> _incidentMasterRepository;
        private readonly ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> _schemaBuildDirector;

        public IncidentResourceController(
            ICommandAvailabilityManager<IncidentCommand> commandAvailabilityManager,
            ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion> schemaBuildDirector,
            IRepository<IncidentMasterModel> incidentMasterRepository)
        {
            _commandAvailabilityManager = commandAvailabilityManager;
            _schemaBuildDirector = schemaBuildDirector;
            _incidentMasterRepository = incidentMasterRepository;
        }

        [Route(ApiRoutes.IncidentSchema.Route, Name = ApiRoutes.IncidentSchema.Name)]
        public dynamic GetSchema(string @class = null)
        {
            var criterion = new IncidentSchemaKeyCriterion {IncidentClass = @class};

            var result = _schemaBuildDirector
                .GetSchemataForNewModel(criterion)
                .Select(item => new Schema
                {
                    Name = item.Name,
                    Fields = item.Members.Select(member => new FieldAttributes(member))
                });

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route(ApiRoutes.IncidentByIdSchema.Route, Name = ApiRoutes.IncidentByIdSchema.Name)]
        public dynamic GetSchema(int id, string @class = null)
        {
            var criterion = new IncidentSchemaKeyCriterion {IncidentClass = @class};

            var result = _schemaBuildDirector
                .GetSchemata(id, criterion)
                .Select(item => new Schema
                {
                    Name = item.Name,
                    Fields = item.Members.Select(member => new FieldAttributes(member)),
                    
                });

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route(ApiRoutes.IncidentResource.Route, Name = ApiRoutes.IncidentResource.Name)]
        public dynamic GetResources()
        {
            var availableCommands = _commandAvailabilityManager.GetForNewModel().ToList();

            var schema = Url.GetHrefFromRouteName(ApiRoutes.IncidentSchema.Name);

            var availableCommandLinks = availableCommands.CreateAvailableCommandLinks(Url);

            var result = CreateResult(schema, availableCommandLinks);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route(ApiRoutes.IncidentByIdResource.Route, Name = ApiRoutes.IncidentByIdResource.Name)]
        public dynamic GetResources(int id)
        {
            var availableCommands = _commandAvailabilityManager.GetByModelId(id);

            var schema = Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdSchema.Name, new { id });

            var availableCommandLinks = availableCommands.CreateAvailableCommandLinks(Url, id);

            var result = CreateResult(schema, availableCommandLinks, id);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private GetIncidentResourcesResult CreateResult(string schema,
            IEnumerable<AvailableCommandLink> availableCommandLinks, int incidentId = 0)
        {
            var result = new GetIncidentResourcesResult
            {
                Schema = schema,
                Authorisation = Url.GetHrefFromRouteName(ApiRoutes.IncidentAuthorisation.Name),
                Commands = availableCommandLinks,
                Odata = GetAvilableOdataResources(incidentId).ToArray(),
                References = new[]
                {
                    new {Key = "products", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentProduct.Name)},
                    new {Key = "countries", HRef = Url.GetHrefFromRouteName(ApiRoutes.Country.Name)},
                    new {Key = "orgTypes", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentOrgType.Name)},
                    new {Key = "riskRatings", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentRiskRating.Name)},
                    new {Key = "residualRiskRatings", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentResidualRiskRating.Name)},
                    new {Key = "actionassignableusers", HRef = incidentId == 0 ? null : Url.GetHrefFromRouteName(ApiRoutes.IncidentByIdActionAssignableUsers.Name, null)},
                    new {Key = "ukviImmediateReportTypes", HRef = Url.GetHrefFromRouteName(ApiRoutes.UkviImmediateReportType.Name)}
                }.Select(item => new NamedLink(item.Key, item.HRef)),
                Trees = new[]
                {
                    new {Key = "adminUnit", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentAdminUnit.Name)},
                    new {Key = "class", HRef = Url.GetHrefFromRouteName(ApiRoutes.IncidentClass.Name)}
                }.Select(item => new NamedLink(item.Key, item.HRef))
            };
            return result;
        }

        private IEnumerable<NamedLink> GetAvilableOdataResources(int incId)
        {
            var returnObject = new List<NamedLink>();
            if (incId == 0) return returnObject;

            //If incident is in progress add the action endpoint
            var incidentMaster = _incidentMasterRepository.GetById(incId);

            if (incidentMaster.Status == IncidentStatus.InProgress || incidentMaster.Status == IncidentStatus.Closed)
            {
                returnObject.Add(new NamedLink("actions",
                    Request.RequestUri.GetLeftPart(UriPartial.Authority) + ApiRoutes.IncidentActions.Route));
            }
            
            if (incidentMaster.Status == IncidentStatus.InProgress || incidentMaster.Status == IncidentStatus.Closed)
            {
                returnObject.Add(new NamedLink("activity",
                    Request.RequestUri.GetLeftPart(UriPartial.Authority) + ApiRoutes.IncidentActivity.Route));
            }

            return returnObject;
        }
    }
}