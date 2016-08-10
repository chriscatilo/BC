using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class AuthorisationController : ApiController
    {
        private readonly IAssetAuthoriser _authoriser;

        public AuthorisationController(IAssetAuthoriser authoriser)
        {
            _authoriser = authoriser;
        }

        [Route(ApiRoutes.SiteAuthorisation.Route, Name = ApiRoutes.SiteAuthorisation.Name)]
        public dynamic Get()
        {
            throw new NotImplementedException();
        }

        [Route(ApiRoutes.IncidentAuthorisation.Route, Name = ApiRoutes.IncidentAuthorisation.Name)]
        public dynamic GetIncident()
        {
            var authorisations = _authoriser.GetUserAuthorisations();

            return Request.CreateResponse(HttpStatusCode.OK, authorisations);
        }

        [Route(ApiRoutes.AuditingAuthorisation.Route, Name = ApiRoutes.AuditingAuthorisation.Name)]
        public dynamic GetAuditiing()
        {
            throw new NotImplementedException();
        }
    }
}