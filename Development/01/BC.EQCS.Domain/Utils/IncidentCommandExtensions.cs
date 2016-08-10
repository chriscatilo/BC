using System;
using BC.EQCS.Domain.Incident;

namespace BC.EQCS.Domain.Utils
{
    public static class IncidentCommandExtensions
    {
        public static String ConvertToSecurityAssetKey(this IncidentCommand command)
        {
            var permissionsMapping = new IncidentCommandSecurityAssetMapping();

            return permissionsMapping[command];
        }
    }
}
