using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Infrastructure.Logging;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class HealthMonitoringController : ApiController
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly ILogger _logger;

        /// <summary>
        /// Used by external British Council Monitoring tools to check health of this Web Application
        /// </summary>
        /// <returns></returns>
        public HealthMonitoringController(IAssetAuthoriser authoriser, ILogger logger)
        {
            _authoriser = authoriser;
            _logger = logger;
        }
        
        /// <summary>
        /// Returns HTTP.OK if the current user is Anonymous and database is accessible for reading and writing.
        /// Database reading tested by virtue of UserContext being established at this point and utilised by Authoriser
        /// Database writing is tested by logging Info. 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(ApiRoutes.HealthMonitoring.Route, Name = ApiRoutes.HealthMonitoring.Name)]
        public HttpResponseMessage Get()
        {
            try
            {
                if (_authoriser.IsAuthorised("DISPLAY_SYSTEM_HEALTH_STATUS"))
                {
                    _logger.Info("Health Check Status: OK");
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception ex )
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}