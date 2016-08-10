namespace BC.EQCS.Security.Service
{
    public interface IIncidentAuthorisor
    {
        bool IsAuthorised(string assetCode, int incidentId);
    }
}