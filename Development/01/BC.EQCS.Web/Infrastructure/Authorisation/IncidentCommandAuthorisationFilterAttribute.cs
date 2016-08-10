using System;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Utils;

namespace BC.EQCS.Web.Infrastructure.Authorisation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IncidentCommandAuthorisationFilterAttribute : UserAuthorisationFilterAttribute
    {
        public IncidentCommandAuthorisationFilterAttribute(IncidentCommand command) 
        {
            _securityAssetKey = command.ConvertToSecurityAssetKey(); 
        }
    }
}