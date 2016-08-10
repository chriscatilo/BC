using System.Linq;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Extensions;
using BC.EQCS.Security.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Security.Service
{
    public class Authoriser : IAssetAuthoriser
    {
        private readonly IContextResolver _contextResolver;
        private readonly IIncidentAuthorisor _incidentAuthorisor;

        public Authoriser(IContextResolver contextResolver, IIncidentAuthorisor incidentAuthorisor)
        {
            _contextResolver = contextResolver;
            _incidentAuthorisor = incidentAuthorisor;
        }
       
        public bool IsAuthorised(string assetCode, int incidentId)
        {
            return _incidentAuthorisor.IsAuthorised(assetCode, incidentId);
        }

        public bool IsAuthorised(string assetCode)
        {
            return (
                        from role in _contextResolver.CurrentUser.ApplicationRoles
                                from asset in role.ApplicationAssets
                        where asset.Code.EqualsCaseInsensitive(assetCode)
                                select asset
                    ).Any();
        }

        public bool IsReadOnly(string categoryCode)
        {
            foreach (var role in _contextResolver.CurrentUser.ApplicationRoles)
            {
                if (!role.ReadOnlyIncidentClasses.Any(x => x.Code.Equals(categoryCode)))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsIncidentClassViewOnly(string incidentClass)
        {
            var roles  = _contextResolver.CurrentUser.ApplicationRoles;

            var viewables = roles.SelectMany(role => role.ViewableIncidentClasses);

            var isViewable = viewables.Any(item => item.Code.EqualsCaseInsensitive(incidentClass));

            var raiseables = roles.SelectMany(role => role.IncidentClasses);

            var isRaisable = raiseables.Any(item => item.Code.EqualsCaseInsensitive(incidentClass));

            return isViewable && !isRaisable;
        }

        public bool IsAuthorised(string assetCode, string adminUnitCode)
        {
            return (
                        from role in _contextResolver.CurrentUser.ApplicationRoles
                        from asset in role.ApplicationAssets
                        where role.AdminUnit.CanAccess(adminUnitCode)
                        where asset.Code.EqualsCaseInsensitive(assetCode)
                        select asset
                    ).Any();
        }

        public bool IsRoleAuthorised(string assetCode, string roleCode, string adminUnitCode)
        {
            return (
                        from role in _contextResolver.CurrentUser.ApplicationRoles
                                from asset in role.ApplicationAssets
                        where role.AdminUnit.CanAccess(adminUnitCode)
                        where asset.Code.EqualsCaseInsensitive(assetCode)
                        where role.ShortCode.EqualsCaseInsensitive(roleCode)
                                select asset
                    ).Any();
        }

        public IncidentAuthorisationModel GetUserAuthorisations()
        {
            var canViewUkviFollowUpDate = IsAuthorised(AssetType.IncidentCanViewUkvi);

            var authorisation = new IncidentAuthorisationModel
            {
                CanViewUkvi = canViewUkviFollowUpDate,

                CanSetUkvi = canViewUkviFollowUpDate
            };

            return authorisation;
        }
    }
}
