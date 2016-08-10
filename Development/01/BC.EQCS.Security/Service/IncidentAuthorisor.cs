using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Extensions;
using BC.EQCS.Security.Models;
using BC.EQCS.Security.Repository;
using BC.EQCS.Utils;

namespace BC.EQCS.Security.Service
{
    public class IncidentAuthorisor : IIncidentAuthorisor
    {
        private readonly IContextResolver _contextResolver;
        private readonly ISecurityIncidentRepository _incidentRepository;
        private string _assetCode;
        private string _testLocation;
        private SecurityIncident _incident;

        public IncidentAuthorisor(IContextResolver contextResolver, ISecurityIncidentRepository incidentRepository)
        {
            _contextResolver = contextResolver;
            _incidentRepository = incidentRepository;
        }

        public bool IsAuthorised(string assetCode, int incidentId)
        {
            _assetCode = assetCode;

            _incident = _incidentRepository.GetIncident(incidentId);

            if (_incident == null)
            {
                return false;
            }

            if (_incident.IsVerification && UserIsVerificationTeamMember)
            {
                if (IsAuthorised(ForVerificationTeamRole)) // that the user may have
                {
                    return true;
                }
            }

            if (IsAuthorised(ForAnyOtherRoles)) // that the user may have
            {
                return true;
            }

            return false;
        }

        private IEnumerable<BC.EQCS.Security.Models.RoleModel> ForAnyOtherRoles
        {
            get
            {
                return _contextResolver.CurrentUser.ApplicationRoles.Where(role => role.ShortCode != RoleType.VerficationTeamCode);
            }
        }

        private IEnumerable<BC.EQCS.Security.Models.RoleModel> ForVerificationTeamRole
        {
            get
            {
                return _contextResolver.CurrentUser.ApplicationRoles.Where(role => role.ShortCode == RoleType.VerficationTeamCode);
            }
        }

        private bool UserIsVerificationTeamMember
        {
            get
            {
                return _contextResolver.CurrentUser.IsVerificationTeamMember;
            }
        }

        private bool IsAuthorised(IEnumerable<BC.EQCS.Security.Models.RoleModel> roles)
        {
            return (
                 from role in roles
                 from asset in role.ApplicationAssets
                 where asset.Code.EqualsCaseInsensitive(_assetCode)
                 where role.AdminUnit.CanAccess(_incident.TestLocation ?? _incident.TestCentre)
                 select asset
                 ).Any();
        }
      
    }
}