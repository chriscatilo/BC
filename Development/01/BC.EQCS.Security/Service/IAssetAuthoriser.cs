using BC.EQCS.Security.Models;

namespace BC.EQCS.Security.Service
{
    public interface IAssetAuthoriser
    {
        bool IsAuthorised(string assetCode, int incidentId);

        bool IsAuthorised(string assetCode);

        bool IsReadOnly(string categoryCode);

        bool IsIncidentClassViewOnly(string incidentClass);

        bool IsAuthorised(string assetCode, string adminUnitCode);

        bool IsRoleAuthorised(string assetCode, string roleCode, string adminUnitCode);

        IncidentAuthorisationModel GetUserAuthorisations();
    }
}